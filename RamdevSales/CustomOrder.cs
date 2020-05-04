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
using System.IO;

namespace RamdevSales
{
    public partial class CustomOrder : Form
    {
        Connection conn = new Connection();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
       
        public CustomOrder()
        {
            InitializeComponent();
            
        }

        public CustomOrder(POSNEW pOSNEW, string billno)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.pOSNEW = pOSNEW;
            bindcustomer();
            txtcusname.Focus();
            options = conn.getdataset("select * from options");
            if (options.Rows.Count > 0)
            {
                if (options.Rows[0]["txtposadd1"].ToString() == "")
                {
                    txtadd1.Visible = false;
                    lbladd1.Visible = false;
                }
                else
                {
                    lbladd1.Text = options.Rows[0]["txtposadd1"].ToString();
                }
                if (options.Rows[0]["txtposadd2"].ToString() == "")
                {
                    txtadd2.Visible = false;
                    lbladd2.Visible = false;
                }
                else
                {
                    lbladd2.Text = options.Rows[0]["txtposadd2"].ToString();
                }
                if (options.Rows[0]["txtposadd3"].ToString() == "")
                {
                    txtadd3.Visible = false;
                    lbladd3.Visible = false;
                }
                else
                {
                    lbladd3.Text = options.Rows[0]["txtposadd3"].ToString();
                }
                if (options.Rows[0]["txtposadd4"].ToString() == "")
                {
                    txtadd4.Visible = false;
                    lbladd4.Visible = false;
                }
                else
                {
                    lbladd4.Text = options.Rows[0]["txtposadd4"].ToString();
                }
                if (options.Rows[0]["txtposadd5"].ToString() == "")
                {
                    txtadd5.Visible = false;
                    lbladd5.Visible = false;
                }
                else
                {
                    lbladd5.Text = options.Rows[0]["txtposadd5"].ToString();
                }
                getorderno();
            }
            if (!string.IsNullOrEmpty(billno))
            {
                DataTable dt= conn.getdataset("select * from BillPOSMaster where isactive=1 and billno='" + billno + "' and isactive=1");
                txtcusname.Text = dt.Rows[0]["customername"].ToString();
              
            }
            else
            {
                pOSNEW.cusdetails();
                if (!string.IsNullOrEmpty(POSNEW.customername1))
                {
                    txtcusname.Text = POSNEW.customername1;
                }
                if (!string.IsNullOrEmpty(POSNEW.customercity1))
                {
                    txtcuscity.Text = POSNEW.customercity1;
                }
                if (!string.IsNullOrEmpty(POSNEW.customermobile1))
                {
                    txtcusmobile.Text = POSNEW.customermobile1;
                }
                if (POSNEW.customername1 != "")
                {
                    btnsubmit.Visible = false;
                }
            }
        }

        private void getorderno()
        {

            try
            {

                String str = conn.ExecuteScalar("select max(orderid) from customorder where isactive=1");
                var charsToRemove = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-" };
                foreach (var c in charsToRemove)
                {
                    str = str.Replace(c, string.Empty);
                }

                Int64 id, count;
                //     Object data = dr[1];
                string prefix = conn.getsinglevalue("select posorderprefix from options");
                if (str == "")
                {

                    id = 1;
                    count = 1;
                }
                else
                {
                    id = Convert.ToInt64(str) + 1;
                    count = Convert.ToInt64(str) + 1;
                }

                lblorderid.Text = count.ToString();
                txtorderno.Text = prefix + count.ToString();

            }
            catch
            {
            }
            finally
            {

            }


        }
        public static string activecontroal = "";
        public static string customername = "";
        public static string customercity = "";
        public static string customermobile = "";
        public static string customerid = "";
        private POSNEW pOSNEW;

        private void CustomOrder_Load(object sender, EventArgs e)
        {

        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcusname.Text != "")
                {
                    customername = txtcusname.Text;

                    customerid = txtcusname.SelectedValue.ToString();
                }
                this.Close();
            }
            catch
            {
                this.Close();
            }

            
        }

        private void txtcusname_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcusname";
        }

        private void txtcuscity_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcuscity";
        }

        private void txtcusmobile_Enter(object sender, EventArgs e)
        {
            activecontroal = "txtcusmobile";
        }

        private void txtcusmobile_KeyPress(object sender, KeyPressEventArgs e)
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

      

     
     
        public void bindcustomer()
        {
            DataTable Accountgroup = conn.getdataset("select * from AccountGroup where id='99'");
            string groupid = Accountgroup.Rows[0]["UnderGroupID"].ToString();
            string qry = "select ClientID,AccountName from ClientMaster where isactive=1 and (groupID=99 or GroupID='" + groupid + "') order by AccountName";
            //SqlCommand cmd1 = new SqlCommand(qry, con);
            //SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = conn.getdataset(qry);
          
            txtcusname.ValueMember = "ClientID";
            txtcusname.DisplayMember = "AccountName";
            txtcusname.DataSource = dt1;
            txtcusname.DropDownWidth = DropDownWidth(dt1);
            txtcusname.SelectedIndex = -1;
        }
        int DropDownWidth(DataTable myCombo)
        {
            int maxWidth = 0;
            int temp = 0;
            Label label1 = new Label();
            for (int i = 0; i < myCombo.Rows.Count; i++)
            {
                label1.Text = myCombo.Rows[i][1].ToString();
                temp = label1.PreferredWidth;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }

            //foreach (var obj in myCombo.Items)
            //{
            //    label1.Text = obj.ToString();
            //    temp = label1.PreferredWidth;
            //    if (temp > maxWidth)
            //    {
            //        maxWidth = temp;
            //    }
            //}
            label1.Dispose();
            return maxWidth + 3;
        }
        public string accountno="";
        DataTable options = new DataTable();
        public void getaccountno()
        {
            try
            {
                DataTable options = conn.getdataset("select * from options");
                if (options.Rows[0]["accountbillno"].ToString() == "Continuous")
                {
                    string str = conn.ExecuteScalar("select max(ClientID) from ClientMaster where isactive=1");
                    Int64 id, count;
                    if (str == "")
                    {

                        id = Convert.ToInt64(1);
                        count = Convert.ToInt64(1);
                    }
                    else
                    {
                        id = Convert.ToInt64(str) + 1;
                        count = Convert.ToInt64(str) + 1;
                    }
                    accountno = options.Rows[0]["accountprefix"].ToString() + count.ToString();
                    
                }
               
               
            }
            catch
            {
            }
        }
        public static string cusname;
        public static string cuscity;
        public static string cuscontact;
        public static string deliverydate;
        public static string orderstatus;
        public static string imageurl;
        public static string add1;
        public static string add2;
        public static string add3;
        public static string add4;
        public static string add5;
        public static string description;
        public static string comments;
        public static string amount;
       
       
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            //cusname = txtcusname.Text;
            //cuscity = txtcuscity.Text;
            //cuscontact = txtcusmobile.Text;
            //deliverydate = DTPFrom.Text;
            //orderstatus = cmborderstatus.Text;
            //imageurl = appPath + iName;
            //add1 = txtadd1.Text;
            //add2 = txtadd2.Text;
            //add3 = txtadd3.Text;
            //add4 = txtadd4.Text;
            //add5 = txtadd5.Text;
            //description = txtdescription.Text;
            //comments = txtcomments.Text;
            //amount = txtamount.Text;
            //pOSNEW.isorder = true;
            string address = string.Empty;
            if (isavail.Rows.Count > 0)
            {
                bool concat = txtcuscity.Text.Contains("'");
                
                if (concat)
                {
                    char[] textArray = txtcuscity.Text.ToCharArray();
                    for (int i = 0; i < txtcuscity.Text.Length; i++)
                    {
                        if (textArray[i].Equals('\''))
                        {
                            address += "'";
                        }
                        address += Convert.ToString(textArray[i]);
                    }
                }
                else
                {
                    address = txtcuscity.Text;
                }

            }
            else
            {
                conn.execute("INSERT INTO [dbo].[ClientMaster]([AccountName],[PrintName],[GroupName],[Opbal],[Dr_cr],[Address],[City],[State],[Phone],[Mobile],[Email],[Cstno],[Vatno],[GstNo],[AdharNo],[GroupID],[statecode],[crelimite],[billlimite],[credaysale],[credaypurchase],[accountnumber],[customertypeid],[customertype],[noteorremarks],[isactive],[ismaintain])VALUES('" + txtcusname.Text + "','" + txtcusname.Text + "','CUSTOMERS','0','CR','" + address + "','" + address + "','','" + txtcusmobile.Text + "','" + txtcusmobile.Text + "','','','','','','99','','','','','','" + accountno + "','1','General','',1,0)");
            }
            // listviewbind();

            string clientid = conn.getsinglevalue("select clientid from clientmaster where AccountName='" + txtcusname.Text + "'");

            conn.execute("INSERT INTO [dbo].[CustomOrder]([orderid],[orderno],[regdate],[clientid],[deldate],[orderstatus],[ordertype],[image],[description],[comments],[amount],[advanced],[pending],[add1],[add2],[add3],[add4],[add5],[isactive],[cashcredit])VALUES('" + lblorderid.Text + "','" + txtorderno.Text + "','" + DateTime.Now.ToString(Master.dateformate) + "','" + clientid + "','" + Convert.ToDateTime(DTPFrom.Text).ToString(Master.dateformate) + "','" + cmborderstatus.Text + "','" + txtordertype.Text + "','" + photo1[1] + "','" + txtdescription.Text + "','" + txtcomments.Text + "','" + txtamount.Text + "','" + txtadvance.Text + "','" + txtpending.Text + "','" + txtadd1.Text + "','" + txtadd2.Text + "','" + txtadd3.Text + "','" + txtadd4.Text + "','" + txtadd5.Text + "',1,'" + cmbcardcash.Text + "')");

            if (txtadvance.Text != "")
            {
                //string str = conn.getsinglevalue("select max(recno) from paymentreceipt where isactive=1 and type='R'");
                //if (str != "")
                //{
                //    str = (Convert.ToInt64(str) + 1).ToString();
                //}
                //else
                //{
                //    str = "1";
                //}
                Guid guid2;
                guid2 = Guid.NewGuid();
                if (cmbcardcash.Text == "Cash")
                    //     conn.execute("INSERT INTO [dbo].[paymentreceipt]([recno],[type],[date],[mode],[chqno],[chqdate],[bankname],[clientid],[totalamount],[discountamt],[netamt],[remarks],[isactive],[billno]) values ('" + str + "','R','" + DateTime.Now.ToString(Master.dateformate) + "','Cash','','','','" + clientid + "','" + txtamount.Text + "','0','" + txtamount.Text + "','Custom Order','1','" + txtorderno.Text + "')");
                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[isactive],[SyncID],[SyncDatetime])VALUES('" + txtorderno.Text + "','" + DateTime.Now.ToString(Master.dateformate) + "','Custom Order','" + clientid + "','" + txtcusname.Text + "','" + txtamount.Text + "','C','Custom Order','Cash',1,'" + guid2 + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
                if (cmbcardcash.Text == "Card")
                    //   conn.execute("INSERT INTO [dbo].[paymentreceipt]([recno],[type],[date],[mode],[chqno],[chqdate],[bankname],[clientid],[totalamount],[discountamt],[netamt],[remarks],[isactive],[billno]) values ('" + str + "','R','" + DateTime.Now.ToString(Master.dateformate) + "','Card','','','','" + clientid + "','" + txtamount.Text + "','0','" + txtamount.Text + "','Custom Order','1','" + txtorderno.Text + "')");
                    conn.execute("INSERT INTO [dbo].[Ledger]([VoucherID],[Date1],[TranType],[AccountID],[AccountName],[Amount],[DC],[ShortNarration],[OT1],[isactive],[SyncID],[SyncDatetime])VALUES('" + txtorderno.Text + "','" + DateTime.Now.ToString(Master.dateformate) + "','Custom Order','" + clientid + "','" + txtcusname.Text + "','" + txtamount.Text + "','C','Custom Order','Card',1,'" + guid2 + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm:ss") + "')");
            }
            MessageBox.Show("Order Added Successfully");
            this.Close();


        }
        public int flag = 0;
       
        DataTable isavail = new DataTable();

        public void getcustdata()
        {
            isavail = conn.getdataset("select * from clientmaster where isactive=1 and accountname='" + txtcusname.Text + "'");
            if (isavail.Rows.Count > 0)
            {
                txtcusmobile.Text = isavail.Rows[0]["mobile"].ToString();
                txtcuscity.Text = isavail.Rows[0]["city"].ToString();

                DTPFrom.Focus();
              
            }
            else
            {
                txtcuscity.Focus();
               
            }
        }
        private void txtcusname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                getcustdata();
            }
        }

        private void txtcuscity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcusmobile.Focus();
            }
        }

        private void txtcusmobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DTPFrom.Focus();
            }
        }

        private void DTPFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmborderstatus.Focus();
            }
        }

        private void cmborderstatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtordertype.Focus();
            }
        }

        private void txtordertype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn1uphoto.Focus();
            }
           
        }

        private void btn1uphoto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtadd1.Visible == true)
                    txtadd1.Focus();
                else if (txtadd2.Visible == true)
                    txtadd2.Focus();
                else if (txtadd3.Visible == true)
                    txtadd3.Focus();
                else if (txtadd4.Visible == true)
                    txtadd4.Focus();
                else if (txtadd5.Visible == true)
                    txtadd5.Focus();
                else
                    txtdescription.Focus();
            }
        }

        private void txtadd1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               if (txtadd2.Visible == true)
                    txtadd2.Focus();
                else if (txtadd3.Visible == true)
                    txtadd3.Focus();
                else if (txtadd4.Visible == true)
                    txtadd4.Focus();
                else if (txtadd5.Visible == true)
                    txtadd5.Focus();
                else
                    txtdescription.Focus();
            }
        }

        private void txtadd2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtadd3.Visible == true)
                    txtadd3.Focus();
                else if (txtadd4.Visible == true)
                    txtadd4.Focus();
                else if (txtadd5.Visible == true)
                    txtadd5.Focus();
                else
                    txtdescription.Focus();
            }
        }

        private void txtadd3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               if (txtadd4.Visible == true)
                    txtadd4.Focus();
                else if (txtadd5.Visible == true)
                    txtadd5.Focus();
                else
                    txtdescription.Focus();
            }
        }

        private void txtadd4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtadd5.Visible == true)
                    txtadd5.Focus();
                else
                    txtdescription.Focus();
            }
        }

        private void txtadd5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdescription.Focus();
            }
        }

        private void txtdescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcomments.Focus();
            }
        }

        private void txtcomments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtamount.Focus();
            }
        }
        public static string[] photo1 = new string[2];
        string appPath,iName;
        private string[] UploadPhoto(int no)
        {
            var resultsToReturn = new string[2];
            #region
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a Image";
            ofd.Filter = "JPG files (*.jpg)|*.jpg|JPEG files (*.jpeg*)|*.jpeg*";
            // ofd.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\ItemImage\";
            if (Directory.Exists(appPath) == false)
            {
                Directory.CreateDirectory(appPath);
            }
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    iName = txtcusname.Text.Trim().Replace(" ", "").Replace("'", "''") + "-Logo-" + no + "-" + DateTime.Now.ToString("MMddyyyyhhmmss") + "" + ofd.SafeFileName;
                    string filepath = ofd.FileName;
                    try
                    {
                        File.Delete(appPath + iName);
                    }
                    catch
                    {
                    }
                    File.Copy(filepath, appPath + iName);
                    //pic1.Image = new Bitmap(ofd.OpenFile());
                    //saveimage(pictureBox1.Image, appPath);
                    resultsToReturn[0] = appPath;
                    resultsToReturn[1] = iName;
                    return resultsToReturn;
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Unable to open file " + exp.Message);
                    resultsToReturn[0] = "Error";
                    resultsToReturn[1] = "Error";
                    return resultsToReturn;
                }
            }
            else
            {

                ofd.Dispose();
                resultsToReturn[0] = "Error";
                resultsToReturn[1] = "Error";
                return resultsToReturn;
            }

            #endregion
        }
        private void btn1uphoto_Click(object sender, EventArgs e)
        {
            try
            {
                photo1 = UploadPhoto(1);
                if (photo1[0] == "Error")
                {
                    MessageBox.Show("Something Wen't Wrong !!");
                }
                else
                {
                    pic1.Image = Image.FromFile(photo1[0] + photo1[1]);
                    //pic1.ImageLocation=photo1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtadvance.Focus();
            }
        }

        private void txtadvance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpending.Focus();
            }
        }

        private void txtpending_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsubmit.Focus();
            }
        }

        private void txtamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtadvance_KeyPress(object sender, KeyPressEventArgs e)
        {
            {

                if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 45 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void txtcusname_SelectedIndexChanged(object sender, EventArgs e)
        {
            getcustdata();
        }

        private void txtadvance_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtadvance.Text) <= Convert.ToDouble(txtamount.Text))
            {
                txtpending.Text = (Convert.ToDouble(txtamount.Text) - Convert.ToDouble(txtadvance.Text)).ToString();
            }
            else
            {
                MessageBox.Show("Pending amount not greater than actual amount");
            }
        }

     

        
       

        
    }
}
