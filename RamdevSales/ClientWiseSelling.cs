using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RamdevSales
{
    public partial class ClientWiseSelling : Form
    {
        SqlConnection con = new SqlConnection("Data Source=" + Environment.MachineName + ";Initial Catalog=Billing;User ID=sa;Password=root");
        public ClientWiseSelling()
        {
            InitializeComponent();
        }
        
        private void ClientWiseSelling_Load(object sender, EventArgs e)
        {
            binddrop();
            bindclient();
        }

        private void bindclient()
        {
            SqlCommand cmd = new SqlCommand("select ClientID,ClientName from ClientMaster ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbclient.ValueMember = "ClientID";
            cmbclient.DisplayMember = "ClientName";
            cmbclient.DataSource = dt;
        }

        private void binddrop()
        {
            SqlCommand cmd = new SqlCommand("select CompanyId,companyname from Companymaster ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            cmbcomp.ValueMember = "CompanyId";
            cmbcomp.DisplayMember = "companyname";
            cmbcomp.DataSource = dt;
        }

        private void bindgrid()
        {
            DataTable dtProd = new DataTable();
            String qry = "select Product_barcode,Product_Name from ProductMaster where CompanyID='" + cmbcomp.SelectedValue + "' order by Product_Name";
            dtProd = gettable(qry);

            //DataTable dtOp = new DataTable();
            //qry = "select p.Product_barcode, p.Product_Name as Desription,sum(ip.qty) as opening from ProductMaster p left outer join InwardProductMstr ip on ip.ProductID=p.ProductID left outer join InwardMstr i on i.InwardID=ip.InwardID where p.CompanyID='" + cmbcomp.SelectedValue + "' and i.InvoiceDate<'" + Convert.ToDateTime(DTPFrom.Text).ToString("dd-MMM-yyyy") + "' group by p.Product_barcode, p.Product_Name order by p.Product_Name";
            //dtOp = gettable(qry);

            //DataTable dtOp1 = new DataTable();
            //qry = "select p.Product_barcode, p.Product_Name as Desription,sum(bp.Product_Qty) as opening from ProductMaster p left outer join BillProductMaster bp on bp.ProductID=p.ProductID inner join BillMaster b on b.Bill_No=bp.Bill_No where p.CompanyID='" + cmbcomp.SelectedValue + "' and b.Bill_Date<'" + Convert.ToDateTime(DTPFrom.Text).ToString("dd-MMM-yyyy") + "' group by p.Product_barcode, p.Product_Name order by p.Product_Name";
            //dtOp1 = gettable(qry);

            //DataTable dtinw = new DataTable();
            //qry = "select p.Product_barcode, p.Product_Name as Desription,sum(ip.qty) as opening from ProductMaster p left outer join InwardProductMstr ip on ip.ProductID=p.ProductID left outer join InwardMstr i on i.InwardID=ip.InwardID where p.CompanyID='" + cmbcomp.SelectedValue + "' and i.InvoiceDate>='" + Convert.ToDateTime(DTPFrom.Text).ToString("dd-MMM-yyyy") + "' and i.InvoiceDate<='" + Convert.ToDateTime(DTPTo.Text).ToString("dd-MMM-yyyy") + "' group by p.Product_barcode, p.Product_Name order by p.Product_Name";
            //dtinw = gettable(qry);


            DataTable dtout = new DataTable();
             qry = "select p.Product_barcode, p.Product_Name as Desription,sum(bp.Product_Qty) as opening from ProductMaster p left outer join BillProductMaster bp on bp.ProductID=p.ProductID inner join BillMaster b on b.Bill_No=bp.Bill_No where p.CompanyID='" + cmbcomp.SelectedValue + "' and b.Bill_Date>='" + Convert.ToDateTime(DTPFrom.Text).ToString("dd-MMM-yyyy") + "' and b.Bill_Date<='" + Convert.ToDateTime(DTPTo.Text).ToString("dd-MMM-yyyy") + "'and b.ClientID='" + cmbclient.SelectedValue + "' group by p.Product_barcode, p.Product_Name order by p.Product_Name";
            dtout = gettable(qry);


            DataTable mainOp = new DataTable();
            mainOp.Columns.Add("ProductName", typeof(string));
            //mainOp.Columns.Add("Opening", typeof(String));
            //mainOp.Columns.Add("inward", typeof(String));
            mainOp.Columns.Add("Selling", typeof(String));
            //mainOp.Columns.Add("Closing", typeof(String));

            DataRow dr;

            for (int i = 0; i < dtProd.Rows.Count; i++)
            {
                dr = mainOp.NewRow();
                dr["ProductName"] = dtProd.Rows[i][1].ToString();
                Double sell = 0;

                //for (int j = 0; j < dtOp.Rows.Count; j++)
                //{
                //    if (dtProd.Rows[i][0].ToString() == dtOp.Rows[j][0].ToString())
                //    {
                //        for (int k = 0; k < dtOp1.Rows.Count; k++)
                //        {
                //            opening = Convert.ToDouble(dtOp.Rows[j][2].ToString());
                //            if (dtProd.Rows[i][0].ToString() == dtOp1.Rows[k][0].ToString())
                //            {
                //                opening = Convert.ToDouble(dtOp.Rows[j][2].ToString()) - Convert.ToDouble(dtOp1.Rows[k][2].ToString());
                //                if (opening < 0)
                //                    opening = 0;
                //                dr["Opening"] = opening.ToString();
                //                break;
                //            }
                //            else
                //            {
                //                dr["Opening"] = opening.ToString();
                //            }
                //        }
                //        break;
                //    }
                //}

                //for (int j = 0; j < dtinw.Rows.Count; j++)
                //{
                //    if (dtProd.Rows[i][0].ToString() == dtinw.Rows[j][0].ToString())
                //    {
                //        dr["inward"] = dtinw.Rows[j][2].ToString();
                //        inw = Convert.ToDouble(dtinw.Rows[j][2].ToString());
                //        break;
                //    }
                //    else
                //    {
                //        dr["inward"] = "0";
                //        inw = 0;
                //    }
                //}

                for (int j = 0; j < dtout.Rows.Count; j++)
                {
                    if (dtProd.Rows[i][0].ToString() == dtout.Rows[j][0].ToString())
                    {
                        dr["Selling"] = dtout.Rows[j][2].ToString();
                        sell = Convert.ToDouble(dtout.Rows[j][2].ToString());
                        break;
                    }
                    else
                    {
                        dr["Selling"] = "0";
                        sell = 0;
                    }
                }

                //double clos = opening + inw - sell;
                //if (clos < 0)
                //    clos = 0;
                //dr["Closing"] = (clos).ToString();
                mainOp.Rows.Add(dr);
            }



            grdshow.DataSource = mainOp;
            grdshow.Columns[0].Width = 355;
            grdshow.Columns[1].Width = 200;
            //grdshow.Columns[2].Width = 200;
            //grdshow.Columns[3].Width = 200;
            //grdshow.Columns[4].Width = 200;
        }

        private DataTable gettable(String qry)
        {
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            bindgrid();
        }

        private void btngenrpt_Click(object sender, EventArgs e)
        {

        }

    }
}
