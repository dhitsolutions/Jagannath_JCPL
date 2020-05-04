using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace RamdevSales
{
    public partial class PaymentStatus : Form
    {
        delegate void SetComboBoxCellType(int iRowIndex);
        bool bIsComboBox = false;
        static int flag;
        static bool selectedcell;
        static String selectcellvalue;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        public PaymentStatus()
        {
            InitializeComponent();
        }

        private void PaymentStatus_Load(object sender, EventArgs e)
        {
            gridbind();
            grdpayment.ReadOnly = false;
            
        }

        private void gridbind()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select bm.Bill_No,c.ClientName,pm.PaymentStatus,pm.PaymentDate,bm.Bill_Net_Amt,pm.ReceivedAmt from paymentmaster pm inner join BillMaster bm on bm.Bill_No=pm.Bill_No inner join ClientMaster c on c.ClientID=bm.ClientID order by bm.Bill_No desc", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                grdpayment.DataSource = dt;
            }
            catch
            {

            }
        }

        private void grdpayment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //this.grdpayment.BeginEdit(true);
                //this.grdpayment.CurrentCell.ReadOnly = false;
                flag = 0;
                selectedcell = grdpayment.Rows[e.RowIndex].Cells[2].Selected;
                if (grdpayment.Rows[e.RowIndex].Cells[2].Selected)
                {

                    //grdpayment.ReadOnly = false;
                    SetComboBoxCellType objChangeCellType = new SetComboBoxCellType(ChangeCellToComboBox);

                    if (e.ColumnIndex == this.grdpayment.Columns["PaymentStatus"].Index)
                    {
                        this.grdpayment.BeginInvoke(objChangeCellType, e.RowIndex);
                        bIsComboBox = false;
                        flag = 1;
                    }
                }
                else if (grdpayment.Rows[e.RowIndex].Cells[5].Selected || grdpayment.Rows[e.RowIndex].Cells[3].Selected)
                {
                    flag = 1;
                }
                else
                {
                    grdpayment.CurrentCell.ReadOnly = true;
                }
            }
            catch
            {
            }
            
        }
        private void ChangeCellToComboBox(int iRowIndex)
        {
            if (bIsComboBox == false)
            {
                DataGridViewComboBoxCell dgComboCell = new DataGridViewComboBoxCell();
                dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                DataTable dt = new DataTable();
             
                dt.Columns.Add("PaymentStatus", typeof(string));
                dt.Rows.Add("Pending");
                dt.Rows.Add("Paid");

                dgComboCell.DataSource = new String[] { "Pending", "Paid" };
                 dgComboCell.DataSource = dt;
                dgComboCell.ValueMember = "PaymentStatus";
                dgComboCell.DisplayMember = "PaymentStatus";

                grdpayment.Rows[iRowIndex].Cells[grdpayment.CurrentCell.ColumnIndex] = dgComboCell;
                bIsComboBox = true;
            }
        }

        private void grdpayment_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (flag == 1)
            {
                con.Open();
                double receivedamt = 0;
                String str = grdpayment.Rows[e.RowIndex].Cells[5].Value.ToString();
                if (str == "")
                {
                }
                else 
                {
                    receivedamt = Convert.ToDouble(grdpayment.Rows[e.RowIndex].Cells[5].Value.ToString());
                }
                SqlCommand cmd = new SqlCommand("Update paymentmaster set PaymentStatus='" + grdpayment.Rows[e.RowIndex].Cells[2].Value.ToString() + "',PaymentDate='" + DateTime.Now.ToString("MM-dd-yyyy") + "',ReceivedAmt='"+receivedamt+"' where Bill_No='" + grdpayment.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                cmd.ExecuteNonQuery();

                flag = 0;
                con.Close();
              
            }
           
        }

        private void grdpayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                
            }
        }

        private void grdpayment_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //ComboBox combo = e.Control as ComboBox;

            //if (combo != null)
            //{
            //    // Remove an existing event-handler, if present, to avoid 
            //    // adding multiple handlers when the editing control is reused.
            //    combo.SelectedIndexChanged -=
            //        new EventHandler(ComboBox_SelectedIndexChanged);

            //    // Add the event handler. 
            //    combo.SelectedIndexChanged +=
            //        new EventHandler(ComboBox_SelectedIndexChanged);
            //}
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedItem.ToString() == "Paid")
            {

            }
        }

        private void grdpayment_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gridbind();
        }

       
    }
}
