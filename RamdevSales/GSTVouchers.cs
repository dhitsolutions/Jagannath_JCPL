using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace RamdevSales
{
    public partial class GSTVouchers : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public string strConnection = ConfigurationManager.ConnectionStrings["qry"].ToString();
        OleDbSettings ods = new OleDbSettings();
        Connection conn = new Connection();
        Printing prn = new Printing();
        Double g3, g4, g5, g6, g7;
        string gdis1, total1, Dprice, tax12;
        string g1, g2, g8;
        private Master master;
        private TabControl tabControl;

        public GSTVouchers()
        {
            InitializeComponent();
        }

        public GSTVouchers(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        public GSTVouchers(GSTVouchersList gSTVouchersList, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.gSTVouchersList = gSTVouchersList;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
        }

        public GSTVouchers(Ledger ledger, Master master, TabControl tabControl, string[] strfinalarray)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.ledger = ledger;
            this.master = master;
            this.tabControl = tabControl;
            this.strfinalarray = strfinalarray;
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
        }
        public void clearall()
        {
            txtorgbilldate.Text = "";
            txtorgbillno.Text = "";
            lblbasictot.Text = "0";
            txttottax.Text = "0";
            txtamt.Text = "0";
            txttotalcharges.Text = "0";
            txtroundoff.Text = "0";
            TxtBillTotal.Text = "0";
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            cmbentry.SelectedIndex = -1;
            cmbterms.SelectedIndex = -1;
            cmbaccountof.SelectedIndex = -1;
            txtvchno.Text = "";
            TxtBillNo.Text = "";
            txttransport.Text = "";
            txtdelieveryat.Text = "";
            txtfraight.Text = "";
            txtvehicleno.Text = "";
            txtgrrrno.Text = "";
            cmbnarration.SelectedIndex = -1;
            BtnPayment.Text = "&Submit";
            LVCHARGES.Items.Clear();
            cmbparty.SelectedIndex = -1;
            cmbtype.SelectedIndex = -1;
            cmbnarration.Text = "";
            this.ActiveControl = TxtRundate;
            sgstper = "";
            cgstper = "";
            igstper = "";
        }
        public static string clientidupdate;
        public static string newduedate;
        public void getduedate()
        {
            try
            {
                DataTable due = conn.getdataset("select * from ClientMaster where isactive=1 and ClientID='" + cmbparty.SelectedValue + "'");
                if (due.Rows.Count > 0)
                {
                    // if (strfinalarray[0] == "S")
                    // {
                    DateTime billdate = TxtRundate.Value;
                    string creditdays = due.Rows[0]["credaysale"].ToString();
                    if (string.IsNullOrEmpty(creditdays))
                    {
                        creditdays = "0";
                    }
                    DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                    newduedate = Convert.ToString(duedate);
                    //}
                    //else if (strfinalarray[0] == "SR")
                    //{
                    //    DateTime billdate = TxtRundate.Value;
                    //    string creditdays = due.Rows[0]["credaysale"].ToString();
                    //    if (string.IsNullOrEmpty(creditdays))
                    //    {
                    //        creditdays = "0";
                    //    }
                    //    DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                    //    newduedate = Convert.ToString(duedate);
                    //}
                    //else if (strfinalarray[0] == "P")
                    //{
                    //    DateTime billdate = TxtRundate.Value;
                    //    string creditdays = due.Rows[0]["credaypurchase"].ToString();
                    //    if (string.IsNullOrEmpty(creditdays))
                    //    {
                    //        creditdays = "0";
                    //    }
                    //    DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                    //    newduedate = Convert.ToString(duedate);
                    //}
                    //else if (strfinalarray[0] == "PR")
                    //{
                    //    DateTime billdate = TxtRundate.Value;
                    //    string creditdays = due.Rows[0]["credaypurchase"].ToString();
                    //    if (string.IsNullOrEmpty(creditdays))
                    //    {
                    //        creditdays = "0";
                    //    }
                    //    DateTime duedate = billdate.AddDays(Convert.ToDouble(creditdays));
                    //    newduedate = Convert.ToString(duedate);
                    //}
                }
            }
            catch
            {
            }
        }
        public static string sgstper, cgstper, igstper;
        public static string statusreg1 = string.Empty;
        public static string Decrypstatus1(string cipherText)
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
                    statusreg1 = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        private void BtnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                BtnPayment.Enabled = false;
                if (BtnPayment.Text != "Update")
                {
                    DataSet ds = ods.getdata("Select * from tblreg");
                    string reg = Convert.ToString(ds.Tables[0].Rows[0]["d16"].ToString());
                    Decrypstatus1(reg);
                    if (statusreg1 == "Edu")
                    {
                        string sale = conn.ExecuteScalar("select count(id) from tblgstvouchermaster where isactive=1");
                        if (sale == "5")
                        {
                            MessageBox.Show("You Are Not Authorized to Add More Then 5 Bill");
                            return;
                        }
                    }
                }
                getduedate();
                if (BtnPayment.Text != "Update")
                {
                    string isexit = conn.ExecuteScalar("select billno from tblgstvouchermaster where isactive=1 and BillType='" + strfinalarray[0] + "' and billno='" + TxtBillNo.Text + "' and party='" + cmbparty.SelectedValue + "'");
                    if (!string.IsNullOrEmpty(isexit))
                    {
                        MessageBox.Show("Bill No. already Avalable Add another");
                        TxtBillNo.Focus();
                        BtnPayment.Enabled = true;
                        return;
                    }
                }
                if (BtnPayment.Text == "Update")
                {

                    conn.execute("Update tblgstvoucherproductmaster set isactive='0' where party='" + clientidupdate + "' and billtype='" + strfinalarray[0] + "' and billno=(select billno from tblgstvouchermaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "')");
                    conn.execute("Update tblgstvoucherchargesmaster set isactive='0' where party='" + clientidupdate + "' and billtype='" + strfinalarray[0] + "' and billno=(select billno from tblgstvouchermaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "')");
                    DataTable dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbtype.Text + "'");
                    if (dt.Rows[0]["Region"].ToString() == "Local")
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            g1 = row.Cells[0].Value.ToString();// TAX %
                            g2 = row.Cells[1].Value.ToString(); // HSN Code
                            g3 = Convert.ToDouble(row.Cells[2].Value.ToString()); // Taxable AMT
                            g4 = Convert.ToDouble(row.Cells[3].Value.ToString()); // SGST AMT
                            g5 = Convert.ToDouble(row.Cells[4].Value.ToString()); // CGST AMT
                            g6 = Convert.ToDouble(row.Cells[5].Value.ToString()); // Add Tax
                            g7 = Convert.ToDouble(row.Cells[6].Value.ToString()); // Net AMT
                            g8 = row.Cells[7].Value.ToString(); // Description
                            if (!string.IsNullOrEmpty(g1))
                            {
                                if (g1 == "TAX FREE")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 5%")
                                {
                                    sgstper = "2.5";
                                    cgstper = "2.5";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 12%")
                                {
                                    sgstper = "6";
                                    cgstper = "6";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 18%")
                                {
                                    sgstper = "9";
                                    cgstper = "9";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 28%")
                                {
                                    sgstper = "14";
                                    cgstper = "14";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 3%")
                                {
                                    sgstper = "1.5";
                                    cgstper = "1.5";
                                    igstper = "0";
                                }
                                else
                                {
                                    MessageBox.Show("Enter Valid Tax Slab");
                                    return;
                                }
                                conn.execute("INSERT INTO [dbo].[tblgstvoucherproductmaster]([taxper],[hsn],[taxableamount],[sgstamt],[cgstamt],[igstamt],[addtax],[netamt],[description],[party],[billtype],[Bill_No],[billno],[isactive],[sgstper],[cgstper],[igstper],[fkid]) VALUES('" + g1 + "','" + g2 + "','" + g3 + "','" + g4 + "','" + g5 + "','" + "0" + "','" + g6 + "','" + g7 + "','" + g8 + "','" + cmbparty.SelectedValue + "','" + strfinalarray[0] + "','" + txtvchno.Text + "','" + TxtBillNo.Text + "','" + "1" + "','" + sgstper + "','" + cgstper + "','" + igstper + "','" + lblid.Text + "')");
                            }
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            g1 = row.Cells[0].Value.ToString();// TAX %
                            g2 = row.Cells[1].Value.ToString(); // HSN Code
                            g3 = Convert.ToDouble(row.Cells[2].Value.ToString()); // Taxable AMT
                            g4 = Convert.ToDouble(row.Cells[3].Value.ToString()); // IGST AMT
                            g6 = Convert.ToDouble(row.Cells[4].Value.ToString()); // Add Tax
                            g7 = Convert.ToDouble(row.Cells[5].Value.ToString()); // Net AMT
                            g8 = row.Cells[6].Value.ToString(); // Description
                            if (!string.IsNullOrEmpty(g1))
                            {
                                if (g1 == "TAX FREE")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 5%")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "5";
                                }
                                else if (g1 == "GST 12%")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "12";
                                }
                                else if (g1 == "GST 18%")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "18";
                                }
                                else if (g1 == "GST 28%")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "28";
                                }
                                else if (g1 == "GST 3%")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "3";
                                }
                                else
                                {
                                    MessageBox.Show("Enter Valid Tax Slab");
                                    return;
                                }
                                conn.execute("INSERT INTO [dbo].[tblgstvoucherproductmaster]([taxper],[hsn],[taxableamount],[sgstamt],[cgstamt],[igstamt],[addtax],[netamt],[description],[party],[billtype],[Bill_No],[billno],[isactive],[sgstper],[cgstper],[igstper],[fkid]) VALUES('" + g1 + "','" + g2 + "','" + g3 + "','" + "0" + "','" + "0" + "','" + g4 + "','" + g6 + "','" + g7 + "','" + g8 + "','" + cmbparty.SelectedValue + "','" + strfinalarray[0] + "','" + txtvchno.Text + "','" + TxtBillNo.Text + "','" + "1" + "','" + sgstper + "','" + cgstper + "','" + igstper + "','" + lblid.Text + "')");
                            }
                        }
                    }
                    for (int i = 0; i < LVCHARGES.Items.Count; i++)
                    {
                        conn.execute("INSERT INTO [dbo].[tblgstvoucherchargesmaster]([perticulars],[remarks],[onvalue],[at],[plusminus],[amount],[party],[billtype],[Bill_No],[billno],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[chargeid],[isactive],[fkid]) VALUES ('" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','" + cmbparty.SelectedValue + "','" + strfinalarray[0] + "','" + txtvchno.Text + "','" + TxtBillNo.Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[11].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[12].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[13].Text.Replace(",", "") + "','" + "1" + "','" + lblid.Text + "')");
                        //conn.execute("INSERT INTO [Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billtype],[billsundryid],[SyncID],[SyncDatetime],[ClientID],[isactive])VALUES('" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[11].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[12].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + LVCHARGES.Items[i].SubItems[13].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + cmbcustname.SelectedValue + "',1)");
                    }
                    conn.execute("Update tblgstvouchermaster set originalbillno='" + txtorgbillno.Text + "',originalbilldate='"+txtorgbilldate.Text+"',date='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',entry='" + cmbentry.Text + "',terms='" + cmbterms.Text + "',party='" + cmbparty.SelectedValue + "',type='" + cmbtype.SelectedValue + "',onaccount='" + cmbaccountof.SelectedValue + "',vchno='" + txtvchno.Text + "',Bill_No='" + txtvchno.Text + "',billno='" + TxtBillNo.Text + "',transportdetails='" + txttransport.Text + "',delieveryat='" + txtdelieveryat.Text + "',fraight='" + txtfraight.Text + "',vehicleno='" + txtvehicleno.Text + "',grrrno='" + txtgrrrno.Text + "',narration='" + cmbnarration.Text + "',billtype='" + strfinalarray[0] + "',totalbasic='" + lblbasictot.Text.Replace(",", "") + "',totxltax='" + txttottax.Text.Replace(",", "") + "',totalamount='" + txtamt.Text.Replace(",", "") + "',totalcharges='" + txttotalcharges.Text.Replace(",", "") + "',totalrounoff='" + txtroundoff.Text.Replace(",", "") + "',totalfinalamount='" + TxtBillTotal.Text.Replace(",", "") + "' where isactive=1 and billtype='" + strfinalarray[0] + "' and id='" + lblid.Text + "'");
                    string formtype = ""; ;
                    if (strfinalarray[0] == "DN")
                    {
                        formtype = "D";
                    }
                    else if (strfinalarray[0] == "CN")
                    {
                        formtype = "C";
                    }
                    else if (strfinalarray[0] == "EXP")
                    {
                        string ftype = conn.ExecuteScalar("select FormType from [PurchasetypeMaster] where isactive=1 and Purchasetypeid='" + cmbtype.SelectedValue + "'");
                        if (ftype == "P")
                        {
                            formtype = "C";
                        }
                        else
                        {
                            formtype = "C";
                        }
                    }
                    else
                    {
                        formtype = strfinalarray[1];
                    }
                    string vno = conn.ExecuteScalar("select voucherid from ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + strfinalarray[2] + "'");
                    if (vno != "0")
                    {
                        conn.execute("UPDATE [dbo].[Ledger] SET [OD1]='" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "', [OT1]='" + cmbterms.Text + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + strfinalarray[2] + "',[AccountID] = '" + cmbparty.SelectedValue + "',[AccountName]='" + cmbparty.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + formtype + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where AccountID='" + clientidupdate + "' and isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + strfinalarray[2] + "'");
                    }
                    else
                    {
                        Guid guid;
                        guid = Guid.NewGuid();
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + strfinalarray[2] + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + formtype + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                    }
                    if (strfinalarray[0] == "EXP")
                    {
                        if (cmbterms.Text == "Cash")
                        {
                            string ftype = conn.ExecuteScalar("select FormType from [PurchasetypeMaster] where isactive=1 and Purchasetypeid='" + cmbtype.SelectedValue + "'");
                            if (ftype == "P")
                            {
                                string bil = conn.ExecuteScalar("select voucherID from Ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "EXPCash Invoice" + "' ");
                                if (string.IsNullOrEmpty(bil))
                                {
                                    Guid guid;
                                    guid = Guid.NewGuid();
                                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "EXPCash Invoice" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "D" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                                }
                                else
                                {
                                    conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='" + "Cash" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + "EXPCash Invoice" + "',[AccountID] = '" + cmbparty.SelectedValue + "',[AccountName]='" + cmbparty.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + "D" + "',[OD1]='" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "Cash Recept" + "' and AccountID='" + clientidupdate + "'");
                                }
                            }
                            else
                            {
                                string bil = conn.ExecuteScalar("select voucherID from Ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "EXPCash Recept" + "' ");
                                if (string.IsNullOrEmpty(bil))
                                {
                                    Guid guid;
                                    guid = Guid.NewGuid();
                                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "EXPCash Recept" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "D" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                                }
                                else
                                {
                                    conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='" + "Cash" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + "EXPCash Recept" + "',[AccountID] = '" + cmbparty.SelectedValue + "',[AccountName]='" + cmbparty.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + "D" + "',[OD1]='" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "EXPCash Recept" + "' and AccountID='" + clientidupdate + "'");
                                }
                            }
                        }
                        else
                        {
                            string ftype = conn.ExecuteScalar("select FormType from [PurchasetypeMaster] where isactive=1 and Purchasetypeid='" + cmbtype.SelectedValue + "'");
                            if (ftype == "P")
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "EXPCash Invoice" + "' and AccountID='" + clientidupdate + "'");
                            }
                            else
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "EXPCash Recept" + "' and AccountID='" + clientidupdate + "'");
                            }
                        }
                    }
                    else if (strfinalarray[0] == "PR")
                    {
                        if (cmbterms.Text == "Cash")
                        {
                            string bil = conn.ExecuteScalar("select voucherID from Ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "PRCash Recept" + "' ");
                            if (string.IsNullOrEmpty(bil))
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "PRCash Recept" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "C" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                            }
                            else
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='" + "Cash" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + "PRCash Recept" + "',[AccountID] = '" + cmbparty.SelectedValue + "',[AccountName]='" + cmbparty.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + "C" + "',[OD1]='" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "PRCash Recept" + "' and AccountID='" + clientidupdate + "'");
                            }
                        }
                        else
                        {
                            conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "PRCash Recept" + "' and AccountID='" + clientidupdate + "'");
                        }
                    }
                    else if (strfinalarray[0] == "SR")
                    {
                        if (cmbterms.Text == "Cash")
                        {
                            string bil = conn.ExecuteScalar("select voucherID from Ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "SRCash Invoice" + "' ");
                            if (string.IsNullOrEmpty(bil))
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "SRCash Invoice" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "D" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                            }
                            else
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='" + "Cash" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + "SRCash Invoice" + "',[AccountID] = '" + cmbparty.SelectedValue + "',[AccountName]='" + cmbparty.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + "D" + "',[OD1]='" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "SRCash Recept" + "' and AccountID='" + clientidupdate + "'");
                            }
                        }
                        else
                        {
                            conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "SRCash Invoice" + "' and AccountID='" + clientidupdate + "'");
                        }
                    }
                    else if (strfinalarray[0] == "S")
                    {
                        if (cmbterms.Text == "Cash")
                        {
                            string bil = conn.ExecuteScalar("select voucherID from Ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "SaleCash Recept" + "' ");
                            if (string.IsNullOrEmpty(bil))
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "SaleCash Recept" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "C" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                            }
                            else
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='" + "Cash" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + "SaleCash Recept" + "',[AccountID] = '" + cmbparty.SelectedValue + "',[AccountName]='" + cmbparty.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + "C" + "',[OD1]='" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "SaleCash Recept" + "' and AccountID='" + clientidupdate + "'");
                            }
                        }
                        else
                        {
                            conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "SaleCash Recept" + "' and AccountID='" + clientidupdate + "'");
                        }
                    }
                    else if (strfinalarray[0] == "P")
                    {
                        if (cmbterms.Text == "Cash")
                        {
                            string bil = conn.ExecuteScalar("select voucherID from Ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "PurchaseCash Invoice" + "' ");
                            if (string.IsNullOrEmpty(bil))
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "PurchaseCash Invoice" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "D" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                            }
                            else
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='" + "Cash" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + "PurchaseCash Invoice" + "',[AccountID] = '" + cmbparty.SelectedValue + "',[AccountName]='" + cmbparty.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + "D" + "',[OD1]='" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "PurchaseCash Recept" + "' and AccountID='" + clientidupdate + "'");
                            }
                        }
                        else
                        {
                            conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "PurchaseCash Invoice" + "' and AccountID='" + clientidupdate + "'");
                        }
                    }
                    else if (strfinalarray[0] == "DN")
                    {
                        if (cmbterms.Text == "Cash")
                        {
                            string bil = conn.ExecuteScalar("select voucherID from Ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "DNCash Recept" + "' ");
                            if (string.IsNullOrEmpty(bil))
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "DNCash Recept" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "C" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                            }
                            else
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='" + "Cash" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + "DNCash Recept" + "',[AccountID] = '" + cmbparty.SelectedValue + "',[AccountName]='" + cmbparty.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + "C" + "',[OD1]='" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "DNCash Recept" + "' and AccountID='" + clientidupdate + "'");
                            }
                        }
                        else
                        {
                            conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "DNCash Recept" + "' and AccountID='" + clientidupdate + "'");
                        }
                    }
                    else if (strfinalarray[0] == "CN")
                    {
                        if (cmbterms.Text == "Cash")
                        {
                            string bil = conn.ExecuteScalar("select voucherID from Ledger where isactive=1 and AccountID='" + clientidupdate + "' and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "CNCash Invoice" + "' ");
                            if (string.IsNullOrEmpty(bil))
                            {
                                Guid guid;
                                guid = Guid.NewGuid();
                                conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "CNCash Invoice" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "D" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                            }
                            else
                            {
                                conn.execute("UPDATE [dbo].[Ledger] SET [OT1]='" + "Cash" + "',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "', [voucherid]='" + TxtBillNo.Text + "',[Date1]='" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "',[TranType] = '" + "CNCash Invoice" + "',[AccountID] = '" + cmbparty.SelectedValue + "',[AccountName]='" + cmbparty.Text + "' ,[Amount] = '" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "',[DC] = '" + "D" + "',[OD1]='" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "CNCash Recept" + "' and AccountID='" + clientidupdate + "'");
                            }
                        }
                        else
                        {
                            conn.execute("UPDATE [dbo].[Ledger] SET [isactive]='0',SyncDatetime='" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "' where isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + "CNCash Invoice" + "' and AccountID='" + clientidupdate + "'");
                        }
                    }
                    MessageBox.Show("Data Updated Successfully.");
                    clearcharitem();
                    clearall();
                }
                else
                {
                    conn.execute("INSERT INTO [dbo].[tblgstvouchermaster]([date],[entry],[terms],[party],[type],[onaccount],[vchno],[Bill_no],[billno],[transportdetails],[delieveryat],[fraight],[vehicleno],[grrrno],[narration],[billtype],[totalbasic],[totxltax],[totalamount],[totalcharges],[totalrounoff],[totalfinalamount],[isactive],[originalbillno],[originalbilldate]) VALUES('" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + cmbentry.Text + "','" + cmbterms.Text + "','" + cmbparty.SelectedValue + "','" + cmbtype.SelectedValue + "','" + cmbaccountof.SelectedValue + "','" + txtvchno.Text + "','" + txtvchno.Text + "','" + TxtBillNo.Text + "','" + txttransport.Text + "','" + txtdelieveryat.Text + "','" + txtfraight.Text + "','" + txtvehicleno.Text + "','" + txtgrrrno.Text + "','" + cmbnarration.Text + "','" + strfinalarray[0] + "','" + lblbasictot.Text.Replace(",", "") + "','" + txttottax.Text.Replace(",", "") + "','" + txtamt.Text.Replace(",", "") + "','" + txttotalcharges.Text.Replace(",", "") + "','" + txtroundoff.Text.Replace(",", "") + "','" + TxtBillTotal.Text.Replace(",", "") + "','" + "1" +"','"+txtorgbillno.Text+"','"+txtorgbilldate.Text+ "')");
                    string fkid = conn.ExecuteScalar("select max(id) from tblgstvouchermaster where isactive=1");
                    if (string.IsNullOrEmpty(fkid))
                    {
                        fkid = "1";
                    }
                    DataTable dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbtype.Text + "'");
                    if (dt.Rows[0]["Region"].ToString() == "Local")
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            g1 = row.Cells[0].Value.ToString();// TAX %
                            g2 = row.Cells[1].Value.ToString(); // HSN Code
                            g3 = Convert.ToDouble(row.Cells[2].Value.ToString()); // Taxable AMT
                            g4 = Convert.ToDouble(row.Cells[3].Value.ToString()); // SGST AMT
                            g5 = Convert.ToDouble(row.Cells[4].Value.ToString()); // CGST AMT
                            g6 = Convert.ToDouble(row.Cells[5].Value.ToString()); // Add Tax
                            g7 = Convert.ToDouble(row.Cells[6].Value.ToString()); // Net AMT
                            g8 = row.Cells[7].Value.ToString(); // Description
                            if (!string.IsNullOrEmpty(g1))
                            {
                                if (g1 == "TAX FREE")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 5%")
                                {
                                    sgstper = "2.5";
                                    cgstper = "2.5";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 12%")
                                {
                                    sgstper = "6";
                                    cgstper = "6";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 18%")
                                {
                                    sgstper = "9";
                                    cgstper = "9";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 28%")
                                {
                                    sgstper = "14";
                                    cgstper = "14";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 3%")
                                {
                                    sgstper = "1.5";
                                    cgstper = "1.5";
                                    igstper = "0";
                                }
                                else
                                {
                                    MessageBox.Show("Enter Valid Tax Slab");
                                    return;
                                }
                                conn.execute("INSERT INTO [dbo].[tblgstvoucherproductmaster]([taxper],[hsn],[taxableamount],[sgstamt],[cgstamt],[igstamt],[addtax],[netamt],[description],[party],[billtype],[Bill_No],[billno],[isactive],[sgstper],[cgstper],[igstper],[fkid]) VALUES('" + g1 + "','" + g2 + "','" + g3 + "','" + g4 + "','" + g5 + "','" + "0" + "','" + g6 + "','" + g7 + "','" + g8 + "','" + cmbparty.SelectedValue + "','" + strfinalarray[0] + "','" + txtvchno.Text + "','" + TxtBillNo.Text + "','" + "1" + "','" + sgstper + "','" + cgstper + "','" + igstper + "','" + fkid + "')");
                                //conn.execute("INSERT INTO [dbo].[tblgstvoucherproductmaster]([taxper],[hsn],[taxableamount],[sgstamt],[cgstamt],[igstamt],[addtax],[netamt],[description],[party],[billtype],[Bill_No],[billno],[isactive]) VALUES('" + g1 + "','" + g2 + "','" + g3 + "','" + g4 + "','" + g5 + "','" + "0" + "','" + g6 + "','" + g7 + "','" + g8 + "','" + cmbparty.SelectedValue + "','" + strfinalarray[0] + "','" + txtvchno.Text + "','" + TxtBillNo.Text + "','" + "1" + "')");
                            }
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            g1 = row.Cells[0].Value.ToString();// TAX %
                            g2 = row.Cells[1].Value.ToString(); // HSN Code
                            g3 = Convert.ToDouble(row.Cells[2].Value.ToString()); // Taxable AMT
                            g4 = Convert.ToDouble(row.Cells[3].Value.ToString()); // IGST AMT
                            g6 = Convert.ToDouble(row.Cells[4].Value.ToString()); // Add Tax
                            g7 = Convert.ToDouble(row.Cells[5].Value.ToString()); // Net AMT
                            g8 = row.Cells[6].Value.ToString(); // Description
                            if (!string.IsNullOrEmpty(g1))
                            {
                                if (g1 == "TAX FREE")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "0";
                                }
                                else if (g1 == "GST 5%")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "5";
                                }
                                else if (g1 == "GST 12%")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "12";
                                }
                                else if (g1 == "GST 18%")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "18";
                                }
                                else if (g1 == "GST 28%")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "28";
                                }
                                else if (g1 == "GST 3%")
                                {
                                    sgstper = "0";
                                    cgstper = "0";
                                    igstper = "3";
                                }
                                else
                                {
                                    MessageBox.Show("Enter Valid Tax Slab");
                                    return;
                                }
                                conn.execute("INSERT INTO [dbo].[tblgstvoucherproductmaster]([taxper],[hsn],[taxableamount],[sgstamt],[cgstamt],[igstamt],[addtax],[netamt],[description],[party],[billtype],[Bill_No],[billno],[isactive],[sgstper],[cgstper],[igstper],[fkid]) VALUES('" + g1 + "','" + g2 + "','" + g3 + "','" + "0" + "','" + "0" + "','" + g4 + "','" + g6 + "','" + g7 + "','" + g8 + "','" + cmbparty.SelectedValue + "','" + strfinalarray[0] + "','" + txtvchno.Text + "','" + TxtBillNo.Text + "','" + "1" + "','" + sgstper + "','" + cgstper + "','" + igstper + "','" + fkid + "')");
                                //conn.execute("INSERT INTO [dbo].[tblgstvoucherproductmaster]([taxper],[hsn],[taxableamount],[sgstamt],[cgstamt],[igstamt],[addtax],[netamt],[description],[party],[billtype],[Bill_No],[billno],[isactive]) VALUES('" + g1 + "','" + g2 + "','" + g3 + "','" + "0" + "','" + "0" + "','" + g4 + "','" + g6 + "','" + g7 + "','" + g8 + "','" + cmbparty.SelectedValue + "','" + strfinalarray[0] + "','" + txtvchno.Text + "','" + TxtBillNo.Text + "','" + "1" + "')");
                            }
                        }
                    }
                    for (int i = 0; i < LVCHARGES.Items.Count; i++)
                    {
                        conn.execute("INSERT INTO [dbo].[tblgstvoucherchargesmaster]([perticulars],[remarks],[onvalue],[at],[plusminus],[amount],[party],[billtype],[Bill_No],[billno],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[chargeid],[isactive],[fkid]) VALUES ('" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','" + cmbparty.SelectedValue + "','" + strfinalarray[0] + "','" + txtvchno.Text + "','" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[11].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[12].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[13].Text.Replace(",", "") + "','" + "1" + "','" + fkid + "')");
                        //conn.execute("INSERT INTO [Billchargesmaster]([billno],[perticulars],[remarks],[value],[at],[plusminus],[amount],[valueofexp],[tax],[sgst],[cgst],[igst],[additax],[addtaxamt],[billtype],[billsundryid],[SyncID],[SyncDatetime],[ClientID],[isactive])VALUES('" + TxtBillNo.Text + "','" + LVCHARGES.Items[i].SubItems[0].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[1].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[2].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[3].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[4].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[5].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[6].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[7].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[8].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[9].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[10].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[11].Text.Replace(",", "") + "','" + LVCHARGES.Items[i].SubItems[12].Text.Replace(",", "") + "','" + strfinalarray[0] + "','" + LVCHARGES.Items[i].SubItems[13].Text.Replace(",", "") + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "','" + cmbcustname.SelectedValue + "',1)");
                    }
                    Guid guid2;
                    guid2 = Guid.NewGuid();
                    string formtype = ""; ;
                    if (strfinalarray[0] == "DN")
                    {
                        formtype = "D";
                    }
                    else if (strfinalarray[0] == "CN")
                    {
                        formtype = "C";
                    }
                    else if (strfinalarray[0] == "EXP")
                    {
                        string ftype = conn.ExecuteScalar("select FormType from [PurchasetypeMaster] where isactive=1 and Purchasetypeid='" + cmbtype.SelectedValue + "'");
                        if (ftype == "P")
                        {
                            formtype = "C";
                        }
                        else
                        {
                            formtype = "C";
                        }
                    }
                    else
                    {
                        formtype = strfinalarray[1];
                    }
                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + strfinalarray[2] + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + formtype + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid2 + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                    if (strfinalarray[0] == "EXP" && cmbterms.Text == "Cash")
                    {
                        string ftype = conn.ExecuteScalar("select FormType from [PurchasetypeMaster] where isactive=1 and Purchasetypeid='" + cmbtype.SelectedValue + "'");
                        if (ftype == "P")
                        {
                            Guid guid;
                            guid = Guid.NewGuid();
                            conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "EXPCash Invoice" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "D" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                        }
                        else
                        {
                            Guid guid;
                            guid = Guid.NewGuid();
                            conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "EXPCash Recept" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "D" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                        }
                        //conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[OD1],[SyncID],[SyncDatetime],[isactive]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','Cash Recept','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','C','" + "Cash Recept" + "','" + "Cash" + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1)");
                    }
                    else if (strfinalarray[0] == "S" && cmbterms.Text == "Cash")
                    {
                        Guid guid;
                        guid = Guid.NewGuid();
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "SaleCash Recept" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "C" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                        //conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[OD1],[SyncID],[SyncDatetime],[isactive]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','Cash Recept','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','C','" + "Cash Recept" + "','" + "Cash" + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1)");
                    }
                    else if (strfinalarray[0] == "P" && cmbterms.Text == "Cash")
                    {
                        Guid guid;
                        guid = Guid.NewGuid();
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "PurchaseCash Invoice" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "D" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                        //conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[OD1],[SyncID],[SyncDatetime],[isactive]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','Cash Invoice','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','D','" + "Cash Payment" + "','" + "Cash" + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1)");
                    }
                    else if (strfinalarray[0] == "PR" && cmbterms.Text == "Cash")
                    {
                        Guid guid;
                        guid = Guid.NewGuid();
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "PRCash Recept" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "C" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                        //conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[OD1],[SyncID],[SyncDatetime],[isactive]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','Cash Recept','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','C','" + "Cash Recept" + "','" + "Cash" + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1)");
                    }
                    else if (strfinalarray[0] == "SR" && cmbterms.Text == "Cash")
                    {
                        Guid guid;
                        guid = Guid.NewGuid();
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "SRCash Invoice" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "D" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                        //conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[OD1],[SyncID],[SyncDatetime],[isactive]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','Cash Invoice','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','D','" + "Cash Payment" + "','" + "Cash" + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1)");
                    }
                    else if (strfinalarray[0] == "DN" && cmbterms.Text == "Cash")
                    {
                        Guid guid;
                        guid = Guid.NewGuid();
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "DNCash Recept" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "C" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                        //conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[OD1],[SyncID],[SyncDatetime],[isactive]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','Cash Recept','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','C','" + "Cash Recept" + "','" + "Cash" + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1)");
                    }
                    else if (strfinalarray[0] == "CN" && cmbterms.Text == "Cash")
                    {
                        Guid guid;
                        guid = Guid.NewGuid();
                        conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[isactive],[OT1],[OD1],[SyncID],[SyncDatetime]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','" + "CNCash Invoice" + "','" + cmbparty.SelectedValue + "','" + cmbparty.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','" + "D" + "',1,'" + cmbterms.Text + "','" + Convert.ToDateTime(newduedate).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                        //conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[OD1],[SyncID],[SyncDatetime],[isactive]) values ('" + TxtBillNo.Text + "','" + Convert.ToDateTime(TxtRundate.Text).ToString(Master.dateformate) + "','Cash Invoice','" + cmbcustname.SelectedValue + "','" + cmbcustname.Text + "','" + Math.Round(Convert.ToDouble(TxtBillTotal.Text), 2).ToString("########.00") + "','D','" + "Cash Payment" + "','" + "Cash" + "','" + Convert.ToDateTime(txtduedate.Text).ToString(Master.dateformate) + "','" + guid + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "',1)");
                    }
                    MessageBox.Show("Data Inserted Successfully.");
                    clearcharitem();
                    clearall();
                }
                BtnPayment.Enabled = true;
            }
            catch
            {
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                conn.execute("Update tblgstvouchermaster set isactive='0' where party='" + clientidupdate + "' and billtype='" + strfinalarray[0] + "' and id='" + lblid.Text + "'");
                conn.execute("Update tblgstvoucherproductmaster set isactive='0' where party='" + clientidupdate + "' and billtype='" + strfinalarray[0] + "' and billno=(select billno from tblgstvouchermaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "')");
                conn.execute("Update tblgstvoucherchargesmaster set isactive='0' where party='" + clientidupdate + "' and billtype='" + strfinalarray[0] + "' and billno=(select billno from tblgstvouchermaster where id='" + lblid.Text + "' and BillType='" + strfinalarray[0] + "')");
                conn.execute("UPDATE [dbo].[Ledger] SET isactive='0' where AccountID='" + clientidupdate + "' and isactive=1 and [VoucherID]= '" + oldbillno + "' and [TranType] = '" + strfinalarray[2] + "'");
                MessageBox.Show("Data Deleted Successfully.");
                clearall();
                master.RemoveCurrentTab();
            }
            catch
            {
            }
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {

        }

        private void TxtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1.Focus();
            }
        }
        int cnt = 0;
        private void GSTVouchers_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet dtrange = ods.getdata("SELECT Company.* FROM Company where CompanyID='" + Master.companyId + "'");
                TxtRundate.MinDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Startdate"].ToString());
                TxtRundate.MaxDate = Convert.ToDateTime(dtrange.Tables[0].Rows[0]["Enddate"].ToString());
                LVCHARGES.Columns.Add("Perticulars", 237, HorizontalAlignment.Left);
                LVCHARGES.Columns.Add("Remarks", 215, HorizontalAlignment.Left);
                LVCHARGES.Columns.Add("Value", 167, HorizontalAlignment.Left);
                LVCHARGES.Columns.Add("@", 122, HorizontalAlignment.Left);
                LVCHARGES.Columns.Add("+/-", 91, HorizontalAlignment.Left);
                LVCHARGES.Columns.Add("Amount", 117, HorizontalAlignment.Right);
                LVCHARGES.Columns.Add("valueofexp", 0, HorizontalAlignment.Right);
                LVCHARGES.Columns.Add("tax", 0, HorizontalAlignment.Right);
                LVCHARGES.Columns.Add("sgst", 0, HorizontalAlignment.Right);
                LVCHARGES.Columns.Add("cgst", 0, HorizontalAlignment.Right);
                LVCHARGES.Columns.Add("igst", 0, HorizontalAlignment.Right);
                LVCHARGES.Columns.Add("additax", 0, HorizontalAlignment.Right);
                LVCHARGES.Columns.Add("additaxamt", 0, HorizontalAlignment.Right);
                LVCHARGES.Columns.Add("chargeID", 0, HorizontalAlignment.Right);
                options = conn.getdataset("select * from options");
                //charges = 1;
                if (cnt == 1)
                {
                    DataTable dt3 = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbtype.Text + "'");
                    if (dt3.Rows[0]["Region"].ToString() == "Local")
                    {
                        dataGridView1.Columns[0].Width = 100;
                        dataGridView1.Columns[1].Width = 100;
                        dataGridView1.Columns[2].Width = 100;
                        dataGridView1.Columns[3].Width = 100;
                        dataGridView1.Columns[4].Width = 100;
                        dataGridView1.Columns[5].Width = 100;
                        dataGridView1.Columns[6].Width = 100;
                        dataGridView1.Columns[7].Width = 100;
                        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                        dataGridView1.Columns.Add(btn);
                        btn.HeaderText = "Delete";
                        // btn.Text = "Delete";
                        btn.Name = "btndelete";
                        btn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns[8].Width = 30;
                        //  dataGridView1.RowHeadersVisible = false;
                    }
                    else
                    {
                        dataGridView1.Columns[0].Width = 100;
                        dataGridView1.Columns[1].Width = 100;
                        dataGridView1.Columns[2].Width = 100;
                        dataGridView1.Columns[3].Width = 100;
                        dataGridView1.Columns[4].Width = 100;
                        dataGridView1.Columns[5].Width = 100;
                        dataGridView1.Columns[6].Width = 100;
                        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                        dataGridView1.Columns.Add(btn);
                        btn.HeaderText = "Delete";
                        // btn.Text = "Delete";
                        btn.Name = "btndelete";
                        btn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns[7].Width = 30;
                        // dataGridView1.RowHeadersVisible = false;
                    }
                }
                TxtRundate.CustomFormat = Master.dateformate;
                this.ActiveControl = TxtRundate;
            }
            catch
            {
            }
        }

        private void TxtRundate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbentry.Focus();
            }
        }

        private void cmbentry_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbentry.SelectedIndex = 0;
                cmbentry.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbterms_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbterms.SelectedIndex = 0;
                cmbterms.DroppedDown = true;
            }
            catch
            {
            }
        }
        public void bindparty()
        {
            try
            {
                string qry = "";
                qry = "select ClientID,AccountName from ClientMaster where isactive=1 order by AccountName asc";
                //if (cmbentry.Text == "Sale")
                //{
                //    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                //    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                //    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID='" + groupid + "') order by AccountName";
                //}
                //else if (cmbentry.Text == "Sale Return")
                //{
                //    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                //    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                //    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID='" + groupid + "') order by AccountName";
                //}
                //else if (cmbentry.Text == "Purchase")
                //{
                //    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                //    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                //    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or GroupID='" + groupid + "') order by AccountName";
                //}
                //else if (cmbentry.Text == "Debit.Note")
                //{
                //    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                //    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                //    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or GroupID='" + groupid + "') order by AccountName";
                //}
                //else if (cmbentry.Text == "Credit.Note")
                //{
                //    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                //    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                //    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or GroupID='" + groupid + "') order by AccountName";
                //}
                //else if (cmbentry.Text == "Purchase Return")
                //{
                //    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                //    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                //    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or GroupID='" + groupid + "') order by AccountName";
                //}
                //else if (cmbentry.Text == "Exp.Voucher")
                //{
                //    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where (id='99' or id='100')");
                //    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                //    string groupid1 = Accountgroup.Rows[1]["UnderGroupID"].ToString();
                //    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID=100 or GroupID='" + groupid + "' or GroupID='" + groupid1 + "') order by AccountName";
                //}
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbparty.ValueMember = "ClientID";
                cmbparty.DisplayMember = "AccountName";
                cmbparty.DataSource = dt1;
                cmbparty.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void bindaccount()
        {
            try
            {
                string qry = "";
                if (cmbentry.Text == "Sale")
                {
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=23) order by AccountName";
                }
                else if (cmbentry.Text == "Sale Return")
                {
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=23) order by AccountName";
                }
                else if (cmbentry.Text == "Purchase")
                {
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=22) order by AccountName";
                }
                else if (cmbentry.Text == "Debit.Note")
                {
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=22) order by AccountName";
                }
                else if (cmbentry.Text == "Credit.Note")
                {
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=22) order by AccountName";
                }
                else if (cmbentry.Text == "Purchase Return")
                {
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=22) order by AccountName";
                }
                else if (cmbentry.Text == "Exp.Voucher")
                {
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=23 or GroupID=22) order by AccountName";
                }
                SqlCommand cmd1 = new SqlCommand(qry, con);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbaccountof.ValueMember = "ClientID";
                cmbaccountof.DisplayMember = "AccountName";
                cmbaccountof.DataSource = dt1;
                cmbaccountof.SelectedIndex = -1;
            }
            catch
            {
            }
        }
        public void bindsaletype()
        {
            string qry = "";
            if (cmbentry.Text == "Sale")
            {
                DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                qry = "select Purchasetypeid,Purchasetypename from PurchasetypeMaster where FormType='" + "S" + "' and isactive='1'";
            }
            else if (cmbentry.Text == "Sale Return")
            {
                DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                qry = "select Purchasetypeid,Purchasetypename from PurchasetypeMaster where FormType='" + "SR" + "' and isactive='1'";
            }
            else if (cmbentry.Text == "Purchase")
            {
                DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                qry = "select Purchasetypeid,Purchasetypename from PurchasetypeMaster where FormType='" + "P" + "' and isactive='1'";
            }
            else if (cmbentry.Text == "Debit.Note")
            {
                DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                qry = "select Purchasetypeid,Purchasetypename from PurchasetypeMaster where FormType='" + "P" + "' and isactive='1'";
            }
            else if (cmbentry.Text == "Credit.Note")
            {
                DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                qry = "select Purchasetypeid,Purchasetypename from PurchasetypeMaster where FormType='" + "P" + "' and isactive='1'";
            }
            else if (cmbentry.Text == "Purchase Return")
            {
                DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                qry = "select Purchasetypeid,Purchasetypename from PurchasetypeMaster where FormType='" + "PR" + "' and isactive='1'";
            }
            else if (cmbentry.Text == "Exp.Voucher")
            {
                DataTable Accountgroup = conn.getdataset("select * from AccountGroup where (id='99' or id='100')");
                string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                string groupid1 = Accountgroup.Rows[1]["UnderGroupID"].ToString();
                qry = "select Purchasetypeid,Purchasetypename from PurchasetypeMaster where FormType='" + "S" + "' or FormType='" + "P" + "' and isactive='1'";
            }
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            cmbtype.ValueMember = "Purchasetypeid";
            cmbtype.DisplayMember = "Purchasetypename";
            cmbtype.DataSource = dt;
            cmbtype.SelectedIndex = -1;

            //  autobind(dt, cmbsaletype);
        }
        public void bindperticular()
        {
            try
            {
                DataTable dt = conn.getdataset("select billsundryid,billsundryname from billsundry where isactive=1");


                cmbcharper.ValueMember = "billsundryid";
                cmbcharper.DisplayMember = "billsundryname";
                cmbcharper.DataSource = dt;
                cmbcharper.SelectedIndex = -1;
                charges = 0;
            }
            catch
            {
            }

        }
        private void cmbentry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                if (BtnPayment.Text != "Update")
                {
                    bindaccount();
                    bindparty();
                    bindsaletype();
                    bindperticular();
                }
              //  cmbterms.Focus();
                if (cmbentry.Text == "Sale")
                {
                    strfinalarray = new string[5] { "S", "D", "GSTVOUCHERS", "S", "" };
                }
                else if (cmbentry.Text == "Sale Return")
                {
                    strfinalarray = new string[5] { "SR", "C", "GSTVOUCHERSR", "SR", "" };
                }
                else if (cmbentry.Text == "Purchase")
                {
                    strfinalarray = new string[5] { "P", "C", "GSTVOUCHERP", "P", "" };
                }
                else if (cmbentry.Text == "Purchase Return")
                {
                    strfinalarray = new string[5] { "PR", "D", "GSTVOUCHERPR", "PR", "" };
                }
                else if (cmbentry.Text == "Debit.Note")
                {
                    strfinalarray = new string[5] { "DN", "D", "GSTVOUCHERDN", "DN", "" };
                }
                else if (cmbentry.Text == "Credit.Note")
                {
                    strfinalarray = new string[5] { "CN", "C", "GSTVOUCHERCN", "CN", "" };
                }
                else if (cmbentry.Text == "Exp.Voucher")
                {
                    strfinalarray = new string[5] { "EXP", "D", "GSTVOUCHEREXP", "EXP", "" };
                }
                if (strfinalarray[0] == "SR" || strfinalarray[0] == "PR")
                {
                    pnlorgbillno.Visible = true;
                    txtorgbillno.Focus();
                }
                else
                {
                    cmbterms.Focus();
                    this.ActiveControl = cmbterms;

                }
            }
        }
        public void caseterms()
        {
            try
            {
                if (cmbterms.Text == "Cash")
                {
                    string qry = "";
                    qry = "select ClientID,AccountName from ClientMaster where isactive=1 order by AccountName asc";
                    //if (Convert.ToBoolean(options.Rows[0]["showcustomersupplierseperate"].ToString()) == true)
                    //{
                    //    if (strfinalarray[0] == "S")
                    //    {
                    //        DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                    //        string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    //        qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                    //    }
                    //    else if (strfinalarray[0] == "SR")
                    //    {
                    //        DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
                    //        string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    //        qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                    //    }
                    //    else if (strfinalarray[0] == "P")
                    //    {
                    //        DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                    //        string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    //        qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                    //        //   qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=100 order by AccountName";
                    //    }
                    //    else if (strfinalarray[0] == "PR")
                    //    {
                    //        DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='100'");
                    //        string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    //        qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupID=11 or GroupID='" + groupid + "') order by AccountName";
                    //        //qry = "select ClientID,AccountName from ClientMaster where isactive=1 and groupID=100 order by AccountName";
                    //    }
                    //}
                    //else
                    //{
                    //    DataTable Accountgroup = conn.getdataset("select * from AccountGroup where (id='99' or id='100')");
                    //    string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
                    //    string groupid1 = Accountgroup.Rows[1]["UnderGroupID"].ToString();
                    //    qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or groupID=11 or GroupID=100 or GroupID='" + groupid + "' or GroupID='" + groupid1 + "') order by AccountName";
                    //    //  qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=100 or groupid=99) order by AccountName";
                    //}


                    SqlCommand cmd1 = new SqlCommand(qry, con);
                    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    cmbparty.ValueMember = "ClientID";
                    cmbparty.DisplayMember = "AccountName";
                    cmbparty.DataSource = dt1;
                    cmbparty.SelectedIndex = -1;
                }
            }
            catch
            {
            }
        }
        private void cmbterms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                caseterms();
                cmbparty.Focus();
            }
        }

        private void cmbparty_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbparty.SelectedIndex = 0;
                cmbparty.DroppedDown = true;
            }
            catch
            {
            }
        }
        public static string activecontroal;
        private void cmbparty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbparty.Items.Count; i++)
                {
                    s = cmbparty.GetItemText(cmbparty.Items[i]);
                    if (s == cmbparty.Text)
                    {
                        inList = true;
                        cmbparty.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbparty.Text = "";
                }
                cmbtype.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                var privouscontroal = cmbparty;
                activecontroal = privouscontroal.Name;
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);

                client.Passed(1);
                //   client.Show();
                master.AddNewTab(client);
            }
            if (e.KeyCode == Keys.F2)
            {
                var privouscontroal = cmbparty;
                activecontroal = privouscontroal.Name;
                string iid = cmbparty.SelectedValue.ToString();

                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
        }

        private void cmbtype_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbtype.SelectedIndex = 0;
                cmbtype.DroppedDown = true;
            }
            catch
            {
            }
        }
        DataTable batch = new DataTable();
        void getsr()
        {
            try
            {

                String str = conn.ExecuteScalar("select max(Bill_No) from tblgstvouchermaster where isactive='1' and billtype='" + strfinalarray[0] + "' and type='" + cmbtype.SelectedValue + "' and id=(select max(id) from tblgstvouchermaster where isactive=1 and BillType='" + strfinalarray[0] + "' and Type='" + cmbtype.SelectedValue + "' )");
                var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                foreach (var c in charsToRemove)
                {
                    str = str.Replace(c, string.Empty);
                }
                string formtype = ""; ;
                if (strfinalarray[0] == "DN")
                {
                    formtype = "P";
                }
                else if (strfinalarray[0] == "CN")
                {
                    formtype = "P";
                }
                else if (strfinalarray[0] == "EXP")
                {
                    string ftype = conn.ExecuteScalar("select FormType from [PurchasetypeMaster] where isactive=1 and Purchasetypeid='" + cmbtype.SelectedValue + "'");
                    formtype = ftype;
                }
                else
                {
                    formtype = strfinalarray[0];
                }
                DataTable dt = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + formtype + "' and Purchasetypeid='" + cmbtype.SelectedValue + "'");
                Int64 id, count;
                //     Object data = dr[1];

                if (str == "")
                {

                    id = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                    count = Convert.ToInt64(dt.Rows[0]["startingno"].ToString());
                }
                else
                {
                    id = Convert.ToInt64(str) + 1;
                    count = Convert.ToInt64(str) + 1;
                }
                txtvchno.Text = count.ToString();
                TxtBillNo.Text = dt.Rows[0]["prefix"].ToString() + count.ToString();

            }
            catch
            {
            }
            finally
            {

            }

        }
        private void cmbtype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // if (dataGridView1.DataSource == null)
                // {.
                if (BtnPayment.Text != "Update")
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();
                    batch = new DataTable();
                    DataTable dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbtype.Text + "'");
                    if (dt.Rows[0]["Region"].ToString() == "Local")
                    {
                        batch.Columns.Add("Tax %", typeof(string));
                        batch.Columns.Add("HSN Code", typeof(string));
                        batch.Columns.Add("Taxable AMT", typeof(double));
                        batch.Columns.Add("SGST AMT", typeof(double));
                        batch.Columns.Add("CGST AMT", typeof(double));
                        batch.Columns.Add("Add Tax", typeof(double));
                        batch.Columns.Add("Net AMT", typeof(double));
                        batch.Columns.Add("Description", typeof(string));
                        dataGridView1.AllowUserToAddRows = false;
                        DataRow dr = batch.NewRow();
                        dr["Tax %"] = "";
                        dr["HSN Code"] = "";
                        dr["Taxable AMT"] = "0";
                        dr["SGST AMT"] = "0";
                        dr["CGST AMT"] = "0";
                        dr["Add Tax"] = "0";
                        dr["Net AMT"] = "0";
                        dr["Description"] = "";
                        batch.Rows.Add(dr);
                        dataGridView1.DataSource = batch;
                        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                        dataGridView1.Columns.Add(btn);
                        btn.HeaderText = "Delete";
                        // btn.Text = "Delete";
                        btn.Name = "btndelete";
                        btn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns[8].Width = 30;
                        dataGridView1.Columns[6].ReadOnly = true;
                    }
                    else
                    {
                        batch.Columns.Add("Tax %", typeof(string));
                        batch.Columns.Add("HSN Code", typeof(string));
                        batch.Columns.Add("Taxable AMT", typeof(double));
                        batch.Columns.Add("IGST AMT", typeof(double));
                        batch.Columns.Add("Add Tax", typeof(double));
                        batch.Columns.Add("Net AMT", typeof(double));
                        batch.Columns.Add("Description", typeof(string));
                        dataGridView1.AllowUserToAddRows = false;
                        DataRow dr = batch.NewRow();
                        dr["Tax %"] = "";
                        dr["HSN Code"] = "";
                        dr["Taxable AMT"] = "0";
                        dr["IGST AMT"] = "0";
                        dr["Add Tax"] = "0";
                        dr["Net AMT"] = "0";
                        dr["Description"] = "";
                        batch.Rows.Add(dr);
                        dataGridView1.DataSource = batch;
                        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                        dataGridView1.Columns.Add(btn);
                        btn.HeaderText = "Delete";
                        // btn.Text = "Delete";
                        btn.Name = "btndelete";
                        btn.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns[7].Width = 30;
                        dataGridView1.Columns[5].ReadOnly = true;
                    }
                    getsr();
                }
                saletype = cmbtype.Text;

                
                string formtype = ""; ;
                if (strfinalarray[0] == "DN")
                {
                    formtype = "P";
                }
                else if (strfinalarray[0] == "CN")
                {
                    formtype = "P";
                }
                else if (strfinalarray[0] == "EXP")
                {
                    string ftype = conn.ExecuteScalar("select FormType from [PurchasetypeMaster] where isactive=1 and Purchasetypeid='" + cmbtype.SelectedValue + "'");
                    formtype = ftype;
                }
                else
                {
                    formtype = strfinalarray[0];
                }
                DataTable reqgst = conn.getdataset("select * from [PurchasetypeMaster] where isactive=1 and FormType='" + formtype + "' and Purchasetypeid='" + cmbtype.SelectedValue + "'");
                DataTable gstno = conn.getdataset("select * from clientmaster where isactive=1 and ClientID='" + cmbparty.SelectedValue + "'");
                if (reqgst.Rows[0]["TaxCalculation"].ToString() == "Tax Invoice")
                {
                    if (gstno.Rows.Count > 0)
                    {

                        if (gstno.Rows[0]["GstNo"].ToString() == "")
                        {
                            MessageBox.Show("You are Issuing Tax Invoice to a party whose GST NO is not Specified Kindly Check");
                            return;
                        }
                        //else
                        //{
                        //    DialogResult dr = MessageBox.Show("Do you want to Save?", "Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //    if (dr == DialogResult.Yes)
                        //    {
                        //        txtpono.Focus();
                        //    }
                        //}
                        cmbaccountof.Focus();
                    }
                }
                else
                {
                    if (gstno.Rows[0]["GstNo"].ToString() != "")
                    {
                        DialogResult dr = MessageBox.Show("You are Issuing Retail Invoice to Taxable person whose GST No.is specified Are you sure to continue?", "GST No.is specified", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            cmbaccountof.Focus();
                        }
                    }
                    else
                    {
                        cmbaccountof.Focus();
                    }


                }
            }

            //  }
        }

        private void cmbaccountof_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbaccountof.SelectedIndex = 0;
                cmbaccountof.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbaccountof_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtvchno.Focus();
            }
        }

        private void txtvchno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBillNo.Focus();
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
            && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbtype.Text + "'");
                if (dt.Rows[0]["Region"].ToString() == "Local")
                {
                    e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                    if (dataGridView1.CurrentCell.ColumnIndex == 2 || dataGridView1.CurrentCell.ColumnIndex == 3 || dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6) //Desired Column
                    {
                        TextBox tb = e.Control as TextBox;
                        if (tb != null)
                        {
                            tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                        }
                    }
                }
                else
                {
                    e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                    if (dataGridView1.CurrentCell.ColumnIndex == 2 || dataGridView1.CurrentCell.ColumnIndex == 3 || dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 5) //Desired Column
                    {
                        TextBox tb = e.Control as TextBox;
                        if (tb != null)
                        {
                            tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                        }
                    }
                }
                if (dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    SqlDataReader dreader;
                    SqlConnection conn1 = new SqlConnection(strConnection);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn1;
                    cmd.CommandType = CommandType.Text;
                    AutoCompleteStringCollection acBusIDSorce = new AutoCompleteStringCollection();
                    cmd.CommandText = "select DISTINCT Taxslabname from TaxSlabMaster where isactive=1";
                    conn1.Open();
                    dreader = cmd.ExecuteReader();
                    if (dreader.HasRows == true)
                    {
                        while (dreader.Read())
                            acBusIDSorce.Add(dreader["Taxslabname"].ToString());
                    }
                    else
                    {
                        MessageBox.Show("Data not Found");
                    }
                    dreader.Close();
                    //ComboBox txtBusID = e.Control as ComboBox;
                    TextBox txtBusID = e.Control as TextBox;
                    if (txtBusID != null)
                    {
                        txtBusID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtBusID.AutoCompleteCustomSource = acBusIDSorce;
                        txtBusID.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    }
                }
                else
                {
                    TextBox txtBusID = e.Control as TextBox;
                    if (txtBusID != null)
                    {
                        txtBusID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtBusID.AutoCompleteCustomSource = null;
                        txtBusID.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    }
                }
            }
            catch
            {
            }
        }
        Double netvalue = 0;
        string taxslab = "";
        Double n, a, t, q, sgst, cgst;
        DataTable dt = new DataTable();
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int columnindex = dataGridView1.CurrentCell.ColumnIndex;
                int rowindex = dataGridView1.CurrentCell.RowIndex;
                dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbtype.Text + "'");
                if (dt.Rows[0]["Region"].ToString() == "Local")
                {
                    if (columnindex == 0)
                    {
                        taxslab = Convert.ToString(dataGridView1.Rows[rowindex].Cells[columnindex].Value);
                        if (taxslab == "TAX FREE")
                        {

                        }
                        else if (taxslab == "GST 5%")
                        {

                        }
                        else if (taxslab == "GST 12%")
                        {

                        }
                        else if (taxslab == "GST 18%")
                        {

                        }
                        else if (taxslab == "GST 28%")
                        {

                        }
                        else if (taxslab == "GST 3%")
                        {

                        }
                        else
                        {
                            MessageBox.Show("Enter Valid Tax Slab");
                            return;
                        }
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    }
                    if (columnindex == 1)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    }
                    if (columnindex == 2)
                    {
                        var value = dataGridView1.Rows[rowindex].Cells[columnindex].Value;
                        try
                        {
                            if (taxslab == "TAX FREE")
                            {
                                netvalue = (Convert.ToDouble(value) * 0) / 100;
                            }
                            else if (taxslab == "GST 5%")
                            {
                                netvalue = (Convert.ToDouble(value) * 5) / 100;
                            }
                            else if (taxslab == "GST 12%")
                            {
                                netvalue = (Convert.ToDouble(value) * 12) / 100;
                            }
                            else if (taxslab == "GST 18%")
                            {
                                netvalue = (Convert.ToDouble(value) * 18) / 100;
                            }
                            else if (taxslab == "GST 28%")
                            {
                                netvalue = (Convert.ToDouble(value) * 28) / 100;
                            }
                            else if (taxslab == "GST 3%")
                            {
                                netvalue = (Convert.ToDouble(value) * 3) / 100;
                            }
                            Double sgst = netvalue / 2;
                            dataGridView1.Rows[rowindex].Cells[columnindex + 1].Value = Convert.ToDecimal(sgst);
                            Double cgst = netvalue / 2;
                            dataGridView1.Rows[rowindex].Cells[columnindex + 2].Value = Convert.ToDecimal(cgst);
                            Double total = Convert.ToDouble(value) + sgst + cgst;
                            dataGridView1.Rows[rowindex].Cells[columnindex + 4].Value = Convert.ToDecimal(total);
                        }
                        catch
                        {
                        }
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 4];

                    }
                    if (columnindex == 3)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    }
                    if (columnindex == 4)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    }
                    if (columnindex == 5)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    }
                    if (columnindex == 6)
                    {

                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    }
                    if (columnindex == 7)
                    {
                        try
                        {
                            n = 0;
                            sgst = 0;
                            cgst = 0;
                            t = 0;
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                n += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                                sgst += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                                cgst += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                                t += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                            }
                            Double tax = sgst + cgst;
                            lblbasictot.Text = n.ToString();
                            txttottax.Text = tax.ToString();
                            txtamt.Text = t.ToString();
                            TxtBillTotal.Text = t.ToString();
                            getOptions(Math.Round(t, 2));
                        }
                        catch
                        {
                        }
                        columnindex = 0;
                        // DataTable dt1 = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbtype.Text + "'");
                        if (dt.Rows[0]["Region"].ToString() == "Local")
                        {
                            dataGridView1.AllowUserToAddRows = false;
                            DataRow dr = batch.NewRow();
                            dr["Tax %"] = "";
                            dr["HSN Code"] = "";
                            dr["Taxable AMT"] = "0";
                            dr["SGST AMT"] = "0";
                            dr["CGST AMT"] = "0";
                            dr["Add Tax"] = "0";
                            dr["Net AMT"] = "0";
                            dr["Description"] = "";
                            batch.Rows.Add(dr);
                            dataGridView1.DataSource = batch;
                            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                            //dataGridView1.Columns.Add(btn);
                            //btn.HeaderText = "Delete";
                            //// btn.Text = "Delete";
                            //btn.Name = "btndelete";
                            //btn.UseColumnTextForButtonValue = true;
                            //dataGridView1.Columns[8].Width = 30;
                        }
                        else
                        {
                            dataGridView1.AllowUserToAddRows = false;
                            DataRow dr = batch.NewRow();
                            dr["Tax %"] = "";
                            dr["HSN Code"] = "";
                            dr["Taxable AMT"] = "0";
                            dr["IGST AMT"] = "0";
                            dr["Add Tax"] = "0";
                            dr["Net AMT"] = "0";
                            dr["Description"] = "";
                            batch.Rows.Add(dr);
                            dataGridView1.DataSource = batch;
                            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                            //dataGridView1.Columns.Add(btn);
                            //btn.HeaderText = "Delete";
                            //// btn.Text = "Delete";
                            //btn.Name = "btndelete";
                            //btn.UseColumnTextForButtonValue = true;
                            //dataGridView1.Columns[8].Width = 30;
                        }
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex + 1].Cells[columnindex];
                    }
                }
                else
                {
                    if (columnindex == 0)
                    {
                        taxslab = Convert.ToString(dataGridView1.Rows[rowindex].Cells[columnindex].Value);
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    }
                    if (columnindex == 1)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    }
                    if (columnindex == 2)
                    {
                        var value = dataGridView1.Rows[rowindex].Cells[columnindex].Value;
                        try
                        {
                            if (taxslab == "TAX FREE")
                            {
                                netvalue = (Convert.ToDouble(value) * 0) / 100;
                            }
                            else if (taxslab == "GST 5%")
                            {
                                netvalue = (Convert.ToDouble(value) * 5) / 100;
                            }
                            else if (taxslab == "GST 12%")
                            {
                                netvalue = (Convert.ToDouble(value) * 12) / 100;
                            }
                            else if (taxslab == "GST 18%")
                            {
                                netvalue = (Convert.ToDouble(value) * 18) / 100;
                            }
                            else if (taxslab == "GST 28%")
                            {
                                netvalue = (Convert.ToDouble(value) * 28) / 100;
                            }
                            else if (taxslab == "GST 3%")
                            {
                                netvalue = (Convert.ToDouble(value) * 3) / 100;
                            }
                            Double igst = netvalue;
                            dataGridView1.Rows[rowindex].Cells[columnindex + 1].Value = Convert.ToDecimal(igst);
                            // Double cgst = netvalue / 2;
                            // dataGridView1.Rows[rowindex].Cells[columnindex + 2].Value = Convert.ToDecimal(cgst);
                            Double total = Convert.ToDouble(value) + igst;
                            dataGridView1.Rows[rowindex].Cells[columnindex + 3].Value = Convert.ToDecimal(total);
                        }
                        catch
                        {
                        }
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 3];

                    }
                    if (columnindex == 3)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    }
                    if (columnindex == 4)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    }
                    if (columnindex == 5)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    }
                    //if (columnindex == 6)
                    //{

                    //    dataGridView1.CurrentCell = dataGridView1.Rows[rowindex].Cells[columnindex + 1];
                    //}
                    if (columnindex == 6)
                    {
                        try
                        {
                            n = 0;
                            a = 0;
                            t = 0;
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                n += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                                a += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                                t += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
                            }
                            lblbasictot.Text = n.ToString();
                            txttottax.Text = a.ToString();
                            txtamt.Text = t.ToString();
                            TxtBillTotal.Text = t.ToString();
                            getOptions(Math.Round(t, 2));
                        }
                        catch
                        {
                        }
                        columnindex = 0;
                        // DataTable dt1 = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbtype.Text + "'");
                        if (dt.Rows[0]["Region"].ToString() == "Local")
                        {
                            dataGridView1.AllowUserToAddRows = false;
                            DataRow dr = batch.NewRow();
                            dr["Tax %"] = "";
                            dr["HSN Code"] = "";
                            dr["Taxable AMT"] = "0";
                            dr["SGST AMT"] = "0";
                            dr["CGST AMT"] = "0";
                            dr["Add Tax"] = "0";
                            dr["Net AMT"] = "0";
                            dr["Description"] = "";
                            batch.Rows.Add(dr);
                            dataGridView1.DataSource = batch;
                            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                            //dataGridView1.Columns.Add(btn);
                            //btn.HeaderText = "Delete";
                            //// btn.Text = "Delete";
                            //btn.Name = "btndelete";
                            //btn.UseColumnTextForButtonValue = true;
                            //dataGridView1.Columns[8].Width = 30;
                        }
                        else
                        {
                            dataGridView1.AllowUserToAddRows = false;
                            DataRow dr = batch.NewRow();
                            dr["Tax %"] = "";
                            dr["HSN Code"] = "";
                            dr["Taxable AMT"] = "0";
                            dr["IGST AMT"] = "0";
                            dr["Add Tax"] = "0";
                            dr["Net AMT"] = "0";
                            dr["Description"] = "";
                            batch.Rows.Add(dr);
                            dataGridView1.DataSource = batch;
                            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                            //dataGridView1.Columns.Add(btn);
                            //btn.HeaderText = "Delete";
                            //// btn.Text = "Delete";
                            //btn.Name = "btndelete";
                            //btn.UseColumnTextForButtonValue = true;
                            //dataGridView1.Columns[8].Width = 30;
                        }
                        dataGridView1.CurrentCell = dataGridView1.Rows[rowindex + 1].Cells[columnindex];
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["btndelete"].Index)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
                totalcalculation();
            }
        }

        private void cmbcharper_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbcharper.SelectedIndex = 0;
                cmbcharper.DroppedDown = true;
            }
            catch
            {
            }
        }
        int charges;
        public static string s;
        private void cmbcharper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true; // This will eliminate the beeping
                    bool inList = false;
                    for (int i = 0; i < cmbcharper.Items.Count; i++)
                    {
                        s = cmbcharper.GetItemText(cmbcharper.Items[i]);
                        if (s == cmbcharper.Text)
                        {
                            inList = true;
                            cmbcharper.Text = s;
                            break;
                        }
                    }
                    if (!inList)
                    {
                        cmbcharper.Text = "";
                    }

                    if (charges != 1)
                    {
                        if (cmbcharper.Text != "")
                        {

                            DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");
                            if (dt.Rows[0]["symbol"].ToString() == "%")
                            {

                                if (dt.Rows[0]["applyon"].ToString() == "Net")
                                {
                                    txtcharval.Enabled = true;
                                    txtcharat.Enabled = true;
                                    double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(TxtBillTotal.Text)) / 100;
                                    txtcharval.Text = Math.Round(value, 2).ToString();
                                    txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                }
                                else if (dt.Rows[0]["applyon"].ToString() == "Basic Amount")
                                {
                                    txtcharval.Enabled = true;
                                    txtcharat.Enabled = true;
                                    double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(lblbasictot.Text)) / 100;
                                    txtcharval.Text = Math.Round(value, 2).ToString();
                                    txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                }
                                else if (dt.Rows[0]["applyon"].ToString() == "Items Total")
                                {
                                    // txtcharval.Enabled = true;
                                    // txtcharat.Enabled = true;
                                    // double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(lbltotpqty.Text)) / 100;
                                    // txtcharval.Text = Math.Round(value, 2).ToString();
                                    // txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                }
                                else if (dt.Rows[0]["applyon"].ToString() == "Taxable Amount")
                                {
                                    // txtcharval.Enabled = true;
                                    // txtcharat.Enabled = true;
                                    // double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(lblbasictot.Text) - Convert.ToDouble(txttotdiscount.Text) - Convert.ToDouble(txttotadis.Text)) / 100;
                                    // txtcharval.Text = Math.Round(value, 2).ToString();
                                    // txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                }
                                else if (dt.Rows[0]["applyon"].ToString() == "Tax Amount")
                                {
                                    txtcharval.Enabled = true;
                                    txtcharat.Enabled = true;
                                    double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txttottax.Text)) / 100;
                                    txtcharval.Text = Math.Round(value, 2).ToString();
                                    txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                }
                                else if (dt.Rows[0]["applyon"].ToString() == "Tax + AddTax")
                                {
                                    //  txtcharval.Enabled = true;
                                    //  txtcharat.Enabled = true;
                                    //  double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txttottax.Text) + Convert.ToDouble(txttotaddvat.Text)) / 100;
                                    //  txtcharval.Text = Math.Round(value, 2).ToString();
                                    //  txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                }
                                else if (dt.Rows[0]["applyon"].ToString() == "Service Charges")
                                {
                                    //   txtcharval.Enabled = true;
                                    //   txtcharat.Enabled = true;
                                    //   double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txttotservice.Text) + Convert.ToDouble(txttotalcharges.Text)) / 100;
                                    //   txtcharval.Text = Math.Round(value, 2).ToString();
                                    //   txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                }
                                else if (dt.Rows[0]["applyon"].ToString() == "Auto Round Off")
                                {
                                    txtcharval.Enabled = true;
                                    txtcharat.Enabled = true;
                                    double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txtroundoff.Text)) / 100;
                                    txtcharval.Text = Math.Round(value, 2).ToString();
                                    txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                }
                                else if (dt.Rows[0]["applyon"].ToString() == "Amount")
                                {
                                    txtcharval.Enabled = true;
                                    txtcharat.Enabled = true;
                                    double value = (Convert.ToDouble(dt.Rows[0]["ON1"].ToString()) * Convert.ToDouble(txtamt.Text)) / 100;
                                    txtcharval.Text = Math.Round(value, 2).ToString();
                                    txtcharat.Text = dt.Rows[0]["percentage"].ToString();
                                }
                            }
                            else
                            {
                                txtcharval.Text = "0";
                                //  txtcharval.Focus();
                                // txtcharval.Enabled = false;
                                // txtcharat.Enabled = false;
                            }

                            txtcharplusminus.Text = dt.Rows[0]["BillSundryType"].ToString();
                            txtcharamt.Text = Math.Round(Convert.ToDouble(txtcharval.Text) * (Convert.ToDouble(txtcharat.Text) / 100), 2).ToString();
                            //txtcharremark.Focus();
                        }
                    }

                }
                catch
                {
                }
                txtcharremark.Focus();
            }
        }
        private void chargescalculator()
        {
            if (txtcharat.Text == "")
            {
                txtcharat.Text = "0";
            }
            txtcharamt.Text = Math.Round(Convert.ToDouble(txtcharval.Text) * (Convert.ToDouble(txtcharat.Text) / 100), 2).ToString();
        }
        private string[] strfinalarray;
        private void txtcharremark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");
                if (dt.Rows[0]["symbol"].ToString() == "%")
                {
                    txtcharval.Focus();
                }
                else
                {
                    if (dt.Rows[0]["OT3"].ToString() == "1")
                    {
                        ChargesPnl cp = new ChargesPnl(txtcharamt, this, strfinalarray);
                        cp.ShowDialog();
                        txtcharamt.Focus();
                    }
                    else
                    {
                        if (txtcharval.Text != "0")
                        {
                            txtcharval.Focus();
                        }
                        else
                        {
                            txtcharamt.Focus();
                        }
                    }
                }


            }
        }

        private void txtcharval_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcharat.Focus();
            }
        }
        public static string taxvalue = "";
        public static string saletype = "";
        public static string lvfvalue, lvftax, lvfsgst, lvfcgst, lvfigst, lvfaddtax, lvfaddtaxamt;
        private void txtcharat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = conn.getdataset("select * from billsundry where isactive=1 and billsundryid='" + cmbcharper.SelectedValue + "'");
                if (dt.Rows[0]["OT3"].ToString() == "1")
                {
                    double val = Convert.ToDouble(txtcharval.Text);
                    double per = Convert.ToDouble(txtcharat.Text);
                    double totalval = val * per / 100;
                    taxvalue = Convert.ToString(totalval);
                    ChargesPnl cp = new ChargesPnl(txtcharamt, this, strfinalarray);
                    cp.ShowDialog();
                    txtcharamt.Focus();
                }
                else
                {
                    txtcharamt.Focus();
                }
            }
        }

        private void txtcharplusminus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcharamt.Focus();
            }
        }

        private void txtcharamt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btncharadditem.Focus();
            }
        }
        private void clearcharitem()
        {
            cmbcharper.Text = "";
            txtcharamt.Text = "";
            txtcharat.Text = "";
            txtcharremark.Text = "";
            txtcharval.Text = "";
            txtcharplusminus.Text = "";
            ChargesPnl.valofexp = "";
            ChargesPnl.tax1 = "";
            ChargesPnl.sgst1 = "";
            ChargesPnl.cgst1 = "";
            ChargesPnl.igst1 = "";
            ChargesPnl.additax = "";
            lvfvalue = "";

        }
        private void calculatetotalcharges()
        {
            Double charges = 0;
            for (int i = 0; i < LVCHARGES.Items.Count; i++)
            {
                if (LVCHARGES.Items[i].SubItems[4].Text == "+")
                {
                    string str = LVCHARGES.Items[i].SubItems[5].Text;
                    //  totalcalculation();

                    charges += Convert.ToDouble(LVCHARGES.Items[i].SubItems[5].Text);
                }
                if (LVCHARGES.Items[i].SubItems[4].Text == "-")
                {
                    string str = LVCHARGES.Items[i].SubItems[5].Text;
                    charges -= Convert.ToDouble(LVCHARGES.Items[i].SubItems[5].Text);
                }

            }
            txttotalcharges.Text = Math.Round(charges, 2).ToString();
            Double ct = Convert.ToDouble(TxtBillTotal.Text);
            //  Double finaltotal = ct + charges;
            getOptions(ct);
        }
        private void totalcalculation()
        {
            try
            {

                Int32 count = 0;
                Double total = 0;
                Double vat = 0, basic = 0, discount = 0;
                Double addvat = 0;
                Double sgsttotal = 0;
                Double cgsttotal = 0;
                Double igsttotal = 0;
                Double totalcess = 0;
                Double pqty = 0, aqty = 0, free = 0;
                DataTable dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbtype.Text + "'");
                if (dt.Rows[0]["Region"].ToString() == "Local")
                {
                    try
                    {
                        n = 0;
                        sgst = 0;
                        cgst = 0;
                        t = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            n += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                            sgst += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                            cgst += Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value);
                            t += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                        }
                        Double tax = sgst + cgst;
                        lblbasictot.Text = n.ToString();
                        txttottax.Text = tax.ToString();
                        txtamt.Text = t.ToString();
                        TxtBillTotal.Text = t.ToString();
                        getOptions(Math.Round(t, 2));
                    }
                    catch
                    {
                    }
                }
                else
                {
                    try
                    {
                        n = 0;
                        a = 0;
                        t = 0;
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            n += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
                            a += Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
                            t += Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
                        }
                        lblbasictot.Text = n.ToString();
                        txttottax.Text = a.ToString();
                        txtamt.Text = t.ToString();
                        TxtBillTotal.Text = t.ToString();
                        getOptions(Math.Round(t, 2));
                    }
                    catch
                    {
                    }
                }
                try
                {
                    for (int j = 0; j < LVCHARGES.Items.Count; j++)
                    {
                        sgsttotal = sgsttotal + Convert.ToDouble(LVCHARGES.Items[j].SubItems[8].Text);
                        cgsttotal = cgsttotal + Convert.ToDouble(LVCHARGES.Items[j].SubItems[9].Text);
                        igsttotal = igsttotal + Convert.ToDouble(LVCHARGES.Items[j].SubItems[10].Text);
                        addvat = addvat + Convert.ToDouble(LVCHARGES.Items[j].SubItems[11].Text);
                    }
                }
                catch
                {
                    sgsttotal += 0;
                    cgsttotal += 0;
                    igsttotal += 0;
                    addvat += 0;
                }
                //     lblbasictot.Text = basic.ToString();
                //     txttottax.Text = Math.Round(vat, 2).ToString("N2");
                //     txtamt.Text = Math.Round(total, 2).ToString("N2");
                lblsgsttotsl.Text = sgsttotal.ToString("");
                lblcgattotal.Text = cgsttotal.ToString("");
                lbligsttotal.Text = igsttotal.ToString("");

                // getOptions(Math.Round(total, 2));



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Messagebox:" + ex.Message);
            }
        }
        DataTable options = new DataTable();
        private GSTVouchersList gSTVouchersList;
        private void getOptions(Double total)
        {

            DataTable dt = options;
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["autoroundoffingstvouchers"].ToString()) == true)
                {

                    double charges = Convert.ToDouble(txttotalcharges.Text);
                    // double cess = Convert.ToDouble(txttotalcess.Text);
                    TxtBillTotal.Text = Math.Round(total + charges, 0).ToString("N2");
                    txtroundoff.Text = Math.Round((Math.Round(Convert.ToDouble(TxtBillTotal.Text), 0) - Convert.ToDouble(total + charges)), 2).ToString();


                }
                else
                {
                    double charges = Convert.ToDouble(txttotalcharges.Text);
                    //  double cess = Convert.ToDouble(txttotalcess.Text);
                    TxtBillTotal.Text = Math.Round(total + charges, 2).ToString("N2");
                    txtroundoff.Text = "0";

                }
            }
        }
        private void btncharadditem_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem li;
                lbliteminfo.Visible = false;
                li = LVCHARGES.Items.Add(cmbcharper.Text);
                li.SubItems.Add(txtcharremark.Text);
                li.SubItems.Add(txtcharval.Text);
                li.SubItems.Add(txtcharat.Text);
                li.SubItems.Add(txtcharplusminus.Text);
                li.SubItems.Add((Math.Round(Convert.ToDouble(txtcharamt.Text), 2).ToString()));
                DataTable dt = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbtype.Text + "'");
                string charge = conn.ExecuteScalar("select BillSundryID from BillSundry where isactive=1 and BillSundryName='" + cmbcharper.Text + "'");
                if (dt.Rows[0]["Region"].ToString() == "Local")
                {
                    if (ChargesPnl.valofexp == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.valofexp);
                    }
                    if (ChargesPnl.tax1 == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.tax1);
                    }
                    if (ChargesPnl.sgst1 == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.sgst1);
                    }
                    if (ChargesPnl.cgst1 == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.cgst1);
                    }
                    if (ChargesPnl.igst1 == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.igst1);
                    }
                    if (ChargesPnl.additax == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.additax);
                    }
                    if (ChargesPnl.additaxamt == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.additaxamt);
                    }
                    li.SubItems.Add(charge);
                }
                else
                {
                    if (ChargesPnl.valofexp == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.valofexp);
                    }
                    if (ChargesPnl.tax1 == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.tax1);
                    }
                    if (ChargesPnl.sgst1 == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.sgst1);
                    }
                    if (ChargesPnl.cgst1 == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.cgst1);
                    }
                    if (ChargesPnl.igst1 == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.igst1);
                    }
                    if (ChargesPnl.additax == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.additax);
                    }
                    if (ChargesPnl.additaxamt == "")
                    {
                        li.SubItems.Add("0");
                    }
                    else
                    {
                        li.SubItems.Add(ChargesPnl.additaxamt);
                    }
                    li.SubItems.Add(charge);
                }



                calculatetotalcharges();

                totalcalculation();
                clearcharitem();
                cmbcharper.Focus();
            }
            catch
            {
            }
        }

        private void txttransport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdelieveryat.Focus();
            }
        }

        private void txtdelieveryat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtfraight.Focus();
            }
        }

        private void txtfraight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtvehicleno.Focus();
            }
        }

        private void txtvehicleno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtgrrrno.Focus();
            }
        }

        private void txtgrrrno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbnarration.Focus();
            }
        }

        private void cmbnarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnPayment.Focus();
            }
        }

        private void btnAddPartyName_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbparty;
            activecontroal = privouscontroal.Name;
            Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
            client.Passed(1);
            master.AddNewTab(client);
        }

        private void btnEditPartyName_Click(object sender, EventArgs e)
        {
            if (cmbparty.Text != "" && cmbparty.Text != null)
            {
                var privouscontroal = cmbparty;
                activecontroal = privouscontroal.Name;
                string iid = cmbparty.SelectedValue.ToString();
                Accountentry client = new Accountentry(this, master, tabControl, activecontroal);
                client.Update(1, iid);
                client.Passed(1);
                //  client.Show();
                master.AddNewTab(client);
            }
            else
            {
                MessageBox.Show("Please Select PartyName.");
            }
        }
        public void open()
        {
            try
            {
                if (LVCHARGES.SelectedItems.Count > 0)
                {
                    cmbcharper.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[0].Text;
                    txtcharremark.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[1].Text;
                    txtcharval.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[2].Text;
                    txtcharat.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[3].Text;
                    txtcharplusminus.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[4].Text;
                    txtcharamt.Text = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[5].Text;
                    lvfvalue = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[6].Text;
                    lvftax = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[7].Text;
                    lvfsgst = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[8].Text;
                    lvfcgst = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[9].Text;
                    lvfigst = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[10].Text;
                    lvfaddtax = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[11].Text;
                    lvfaddtaxamt = LVCHARGES.Items[LVCHARGES.FocusedItem.Index].SubItems[12].Text;
                    LVCHARGES.Items[LVCHARGES.FocusedItem.Index].Remove();
                    calculatetotalcharges();
                    totalcalculation();
                    cmbcharper.Focus();

                }
            }
            catch
            {
            }
        }

        private void LVCHARGES_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            open();
        }

        private void LVCHARGES_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                open();
            }
            if (e.KeyCode == Keys.Delete)
            {
                LVCHARGES.Items[LVCHARGES.FocusedItem.Index].Remove();
                calculatetotalcharges();
                totalcalculation();

            }
        }

        private void txtcharval_TextChanged(object sender, EventArgs e)
        {
            if (txtcharval.Text != "")
            {
                chargescalculator();
            }
        }

        private void txtcharat_TextChanged(object sender, EventArgs e)
        {
            if (txtcharat.Text != "")
            {
                chargescalculator();
            }
        }

        string oldbillno = "";
        private Ledger ledger;
        internal void updatemode(string str, string p, int clientid, string[] strfinalarray)
        {
            cnt = 1;
            options = conn.getdataset("select * from options");
            DataTable dt = conn.getdataset("select * from tblgstvouchermaster where isactive=1 and party='" + clientid + "' and billno='" + p + "' and billtype='" + strfinalarray[0] + "'");
            DataTable dt1 = conn.getdataset("select * from tblgstvoucherproductmaster where isactive=1 and party='" + clientid + "' and billno='" + p + "' and billtype='" + strfinalarray[0] + "'");
            DataTable dt2 = conn.getdataset("select * from tblgstvoucherchargesmaster where isactive=1 and party='" + clientid + "' and billno='" + p + "' and billtype='" + strfinalarray[0] + "'");
            cmbterms.Text = dt.Rows[0]["Terms"].ToString();
            cmbentry.Text = dt.Rows[0]["entry"].ToString();
            bindaccount();
            bindparty();
            bindsaletype();
            bindperticular();
            caseterms();
            try
            {
                TxtRundate.Text = Convert.ToDateTime(dt.Rows[0]["Date"].ToString()).ToString(Master.dateformate);
            }
            catch
            {
            }
            if (strfinalarray[0] == "SR" || strfinalarray[0] == "PR")
            {
                pnlorgbillno.Visible = true;
                txtorgbilldate.Text = dt.Rows[0]["originalbilldate"].ToString();
                txtorgbillno.Text = dt.Rows[0]["originalbillno"].ToString();
            }
            string clientname = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["party"].ToString() + "' and isactive=1");
            cmbparty.Text = clientname;
            string formtype = ""; ;
            if (strfinalarray[0] == "DN")
            {
                formtype = "P";
            }
            else if (strfinalarray[0] == "CN")
            {
                formtype = "P";
            }
            else if (strfinalarray[0] == "EXP")
            {
                string ftype = conn.ExecuteScalar("select FormType from [PurchasetypeMaster] where isactive=1 and Purchasetypeid='" + dt.Rows[0]["Type"].ToString() + "'");
                formtype = ftype;
            }
            else
            {
                formtype = strfinalarray[0];
            }
            string saletypename = conn.ExecuteScalar("select Purchasetypename from Purchasetypemaster where isactive=1 and FormType='" + formtype + "' and Purchasetypeid='" + dt.Rows[0]["Type"].ToString() + "'");
            cmbtype.Text = saletypename;
            string accountoff = conn.ExecuteScalar("select accountname from clientmaster where isactive=1 and clientid='" + dt.Rows[0]["onaccount"].ToString() + "' and isactive=1");
            cmbaccountof.Text = accountoff;
            clientidupdate = dt.Rows[0]["party"].ToString();
            lblid.Text = dt.Rows[0]["id"].ToString();
            txtvchno.Text = dt.Rows[0]["vchno"].ToString();
            oldbillno = dt.Rows[0]["billno"].ToString();
            TxtBillNo.Text = dt.Rows[0]["billno"].ToString();
            txttransport.Text = dt.Rows[0]["transportdetails"].ToString();
            txtdelieveryat.Text = dt.Rows[0]["delieveryat"].ToString();
            txtfraight.Text = dt.Rows[0]["fraight"].ToString();
            txtvehicleno.Text = dt.Rows[0]["vehicleno"].ToString();
            txtgrrrno.Text = dt.Rows[0]["grrrno"].ToString();
            cmbnarration.Text = dt.Rows[0]["narration"].ToString();
            //   dataGridView1.DataSource = null;
            //   dataGridView1.Columns.Clear();
            //   dataGridView1.Rows.Clear();
            batch = new DataTable();
            DataTable dt3 = conn.getdataset("select * from PurchasetypeMaster where isactive=1 and Purchasetypename='" + cmbtype.Text + "'");
            if (dt3.Rows[0]["Region"].ToString() == "Local")
            {
                batch.Columns.Add("Tax %", typeof(string));
                batch.Columns.Add("HSN Code", typeof(string));
                batch.Columns.Add("Taxable AMT", typeof(double));
                batch.Columns.Add("SGST AMT", typeof(double));
                batch.Columns.Add("CGST AMT", typeof(double));
                batch.Columns.Add("Add Tax", typeof(double));
                batch.Columns.Add("Net AMT", typeof(double));
                batch.Columns.Add("Description", typeof(string));


                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    //DataRow dr = batch.NewRow();
                    //dr["Tax %"] = dt1.Rows[i]["taxper"].ToString();
                    //dr["HSN Code"] = dt1.Rows[i]["hsn"].ToString();
                    //dr["Taxable AMT"] = dt1.Rows[i]["taxableamount"].ToString();
                    //dr["SGST AMT"] = dt1.Rows[i]["sgstamt"].ToString();
                    //dr["CGST AMT"] = dt1.Rows[i]["cgstamt"].ToString();
                    //dr["Add Tax"] = dt1.Rows[i]["addtax"].ToString();
                    //dr["Net AMT"] = dt1.Rows[i]["netamt"].ToString();
                    //dr["Description"] = dt1.Rows[i]["description"].ToString();
                    //batch.Rows.Add(dr);
                    batch.Rows.Add(dt1.Rows[i]["taxper"].ToString(), dt1.Rows[i]["hsn"].ToString(), dt1.Rows[i]["taxableamount"].ToString(), dt1.Rows[i]["sgstamt"].ToString(), dt1.Rows[i]["cgstamt"].ToString(), dt1.Rows[i]["addtax"].ToString(), dt1.Rows[i]["netamt"].ToString(), dt1.Rows[i]["description"].ToString());
                }

                dataGridView1.DataSource = batch;
                dataGridView1.Columns[6].ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;

            }
            else
            {
                batch.Columns.Add("Tax %", typeof(string));
                batch.Columns.Add("HSN Code", typeof(string));
                batch.Columns.Add("Taxable AMT", typeof(double));
                batch.Columns.Add("IGST AMT", typeof(double));
                batch.Columns.Add("Add Tax", typeof(double));
                batch.Columns.Add("Net AMT", typeof(double));
                batch.Columns.Add("Description", typeof(string));
                dataGridView1.AllowUserToAddRows = false;

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    //DataRow dr = batch.NewRow();
                    //dr["Tax %"] = dt1.Rows[i]["taxper"].ToString();
                    //dr["HSN Code"] = dt1.Rows[i]["hsn"].ToString();
                    //dr["Taxable AMT"] = dt1.Rows[i]["taxableamount"].ToString();
                    //dr["IGST AMT"] = dt1.Rows[i]["igstamt"].ToString();
                    //dr["Add Tax"] = dt1.Rows[i]["addtax"].ToString();
                    //dr["Net AMT"] = dt1.Rows[i]["netamt"].ToString();
                    //dr["Description"] = dt1.Rows[i]["description"].ToString();
                    //batch.Rows.Add(dr);
                    batch.Rows.Add(dt1.Rows[i]["taxper"].ToString(), dt1.Rows[i]["hsn"].ToString(), dt1.Rows[i]["taxableamount"].ToString(), dt1.Rows[i]["igstamt"].ToString(), dt1.Rows[i]["addtax"].ToString(), dt1.Rows[i]["netamt"].ToString(), dt1.Rows[i]["description"].ToString());
                }

                dataGridView1.DataSource = batch;
                dataGridView1.Columns[5].ReadOnly = true;

            }
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                ListViewItem li;
                li = LVCHARGES.Items.Add(dt2.Rows[i]["perticulars"].ToString());
                li.SubItems.Add(dt2.Rows[i]["remarks"].ToString());
                li.SubItems.Add(dt2.Rows[i]["onvalue"].ToString());
                li.SubItems.Add(dt2.Rows[i]["at"].ToString());
                li.SubItems.Add(dt2.Rows[i]["plusminus"].ToString());
                li.SubItems.Add(dt2.Rows[i]["amount"].ToString());
                li.SubItems.Add(dt2.Rows[i]["valueofexp"].ToString());
                li.SubItems.Add(dt2.Rows[i]["tax"].ToString());
                li.SubItems.Add(dt2.Rows[i]["sgst"].ToString());
                li.SubItems.Add(dt2.Rows[i]["cgst"].ToString());
                li.SubItems.Add(dt2.Rows[i]["igst"].ToString());
                li.SubItems.Add(dt2.Rows[i]["additax"].ToString());
                li.SubItems.Add(dt2.Rows[i]["addtaxamt"].ToString());
                li.SubItems.Add(dt2.Rows[i]["chargeid"].ToString());
            }
            calculatetotalcharges();
            totalcalculation();
            BtnPayment.Text = "Update";
        }

        private void btnAddPerticular_Click(object sender, EventArgs e)
        {
            var privouscontroal = cmbcharper;
            activecontroal = privouscontroal.Name;
            ChargesHead popup = new ChargesHead(this, cmbcharper.Text, master, tabControl, activecontroal);
            master.AddNewTab(popup);
        }

        private void btnEditPerticular_Click(object sender, EventArgs e)
        {
            if (cmbcharper.Text != "" && cmbcharper.Text != null)
            {
                var privouscontroal = cmbcharper;
                activecontroal = privouscontroal.Name;
                ChargesHead popup = new ChargesHead(this, cmbcharper.Text, master, tabControl, activecontroal);
                popup.Update(1, cmbcharper.Text);
                master.AddNewTab(popup);
            }
            else
            {
                MessageBox.Show("Please Select Perticular Charges");
            }
        }

        private void cmbparty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool inList = false;
                for (int i = 0; i < cmbparty.Items.Count; i++)
                {
                    s = cmbparty.GetItemText(cmbparty.Items[i]);
                    if (s == cmbparty.Text)
                    {
                        inList = true;
                        cmbparty.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbparty.Text = "";
                }
                // getduedate();
            }
            catch (Exception excp)
            {
            }
        }

        private void cmbparty_Leave(object sender, EventArgs e)
        {
            cmbparty.Text = s;
        }

        private void txtorgbillno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtorgbilldate.Focus();
            }
        }

        private void txtorgbilldate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbterms.Focus();
                pnlorgbillno.Visible = false;
            }
        }
    }
}
