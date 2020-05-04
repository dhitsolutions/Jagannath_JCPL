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
    public partial class ProductionPlanning : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["qry"].ToString());
        Connection conn = new Connection();
        private Master master;
        private TabControl tabControl;
        ListViewItem li;
        public ProductionPlanning()
        {
            InitializeComponent();
        }

        public ProductionPlanning(Master master, TabControl tabControl)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.master = master;
            this.tabControl = tabControl;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                master.RemoveCurrentTab();
            }
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
        public void bindprocess()
        {
            SqlCommand cmd = new SqlCommand("select id,processname from tblprocessmaster where isactive=1 and isactiveprocess=1", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);

            cmbprocessname.ValueMember = "id";
            cmbprocessname.DisplayMember = "processname";
            cmbprocessname.DataSource = dt11;
            cmbprocessname.SelectedIndex = -1;
        }
        public void binditem()
        {
            SqlCommand cmd = new SqlCommand("select ProductID,Product_Name from ProductMaster where isactive=1 order by Product_Name", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt11 = new DataTable();
            sda.Fill(dt11);

            cmbitemtoproduce.ValueMember = "ProductID";
            cmbitemtoproduce.DisplayMember = "Product_Name";
            cmbitemtoproduce.DataSource = dt11;
            cmbitemtoproduce.SelectedIndex = -1;
        }
        private void ProductionPlanning_Load(object sender, EventArgs e)
        {
            try
            {
                lvmainitem.Columns.Add("Item Name", 500, HorizontalAlignment.Left);
                lvmainitem.Columns.Add("Process", 300, HorizontalAlignment.Left);
                lvmainitem.Columns.Add("Qty", 100, HorizontalAlignment.Left);
                lvmainitem.Columns.Add("Unit", 100, HorizontalAlignment.Left);
                lvaltitem.Columns.Add("Item Name", 500, HorizontalAlignment.Left);
                lvaltitem.Columns.Add("Qty.Req.", 150, HorizontalAlignment.Left);
                lvaltitem.Columns.Add("Available", 150, HorizontalAlignment.Left);
                lvaltitem.Columns.Add("Deficiency", 150, HorizontalAlignment.Left);
                lvaltitem.Columns.Add("Unit", 100, HorizontalAlignment.Left);
                lvaltitem.Columns.Add("ItemID",0, HorizontalAlignment.Left);
                binditem();
                bindprocess();
                this.ActiveControl = cmbitemtoproduce;
            }
            catch
            {
            }
        }

        private void cmbitemtoproduce_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbitemtoproduce.SelectedIndex = 0;
                cmbitemtoproduce.DroppedDown = true;
            }
            catch
            {
            }
        }
        public void getitemqtyunits()
        {
            DataTable proid = conn.getdataset("select * from ProductMaster where isactive=1 and ProductID='" + cmbitemtoproduce.SelectedValue + "'");
            txtpunit.Text = proid.Rows[0]["Unit"].ToString();
        }
        private void cmbitemtoproduce_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbitemtoproduce.Items.Count; i++)
                {
                    s = cmbitemtoproduce.GetItemText(cmbitemtoproduce.Items[i]);
                    if (s == cmbitemtoproduce.Text)
                    {
                        inList = true;
                        cmbitemtoproduce.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbitemtoproduce.Text = "";
                }
                cmbprocessname.Focus();
                getitemqtyunits();
            }
        }
        public static string s;
        private void cmbprocessname_Enter(object sender, EventArgs e)
        {
            try
            {
                cmbprocessname.SelectedIndex = 0;
                cmbprocessname.DroppedDown = true;
            }
            catch
            {
            }
        }

        private void cmbprocessname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // This will eliminate the beeping
                bool inList = false;
                for (int i = 0; i < cmbprocessname.Items.Count; i++)
                {
                    s = cmbprocessname.GetItemText(cmbprocessname.Items[i]);
                    if (s == cmbprocessname.Text)
                    {
                        inList = true;
                        cmbprocessname.Text = s;
                        break;
                    }
                }
                if (!inList)
                {
                    cmbprocessname.Text = "";
                }
                txtpqty.Focus();
                txtpqty.Text = "1";
            }
        }

        private void txtpqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpunit.Focus();
            }
        }

        private void txtpunit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnaddraw.Focus();
            }
        }

        private void cmbitemtoproduce_Leave(object sender, EventArgs e)
        {
            cmbitemtoproduce.Text = s;
        }

        private void cmbprocessname_Leave(object sender, EventArgs e)
        {
            cmbprocessname.Text = s;
        }
        string name, updateqty, avail, def, itemid;
        public void binddata()
        {
            try
            {
                lvaltitem.Items.Clear();
                for (int i = 0; i < lvmainitem.Items.Count; i++)
                {
                    DataTable dt = conn.getdataset("select * from tblprocessmaster where isactive=1 and processname='" + lvmainitem.Items[i].SubItems[1].Text.Replace(",", "") + "'");
                    DataTable dt1 = conn.getdataset("select * from tblrowmaterialsmaster where isactive=1 and processid='" + dt.Rows[0]["id"].ToString() + "'");
                    for (int a = 0; a < dt1.Rows.Count; a++)
                    {
                        string proid = conn.ExecuteScalar("select ProductID from ProductMaster where isactive=1 and Product_Name='" + dt1.Rows[a]["rowitemname"].ToString() + "'");
                        DataTable SPdt = conn.getdataset("select * from billproductmaster where isactive=1 and productid='" + proid + "'");
                        Double debit = 0, credit = 0;


                        bool exists = false;
                        foreach (ListViewItem item in lvaltitem.Items)
                        {
                            for (int b = 0; b < item.SubItems.Count; b++)
                            {
                                string pid = item.SubItems[5].Text;
                                if (proid == pid)
                                {
                                    name = item.SubItems[0].Text;
                                    updateqty = item.SubItems[1].Text;
                                    avail = item.SubItems[2].Text;
                                    def = item.SubItems[3].Text;
                                    itemid = item.SubItems[4].Text;

                                    rowid1 = item.Index;
                                    exists = true;

                                }


                                //MessageBox.Show(dueDate);
                            }

                        }
                        if (!exists)
                        {
                            #region
                            li = lvaltitem.Items.Add(dt1.Rows[a]["rowitemname"].ToString());
                            for (int j = 0; j < SPdt.Rows.Count; j++)
                            {
                                if (SPdt.Rows[j]["Billtype"].ToString() == "S")
                                {
                                    debit += Convert.ToDouble(SPdt.Rows[j]["Pqty"].ToString());
                                }
                                if (SPdt.Rows[j]["Billtype"].ToString() == "SR")
                                {
                                    credit += Convert.ToDouble(SPdt.Rows[j]["Pqty"].ToString());
                                }
                                if (SPdt.Rows[j]["Billtype"].ToString() == "P")
                                {
                                    credit += Convert.ToDouble(SPdt.Rows[j]["Pqty"].ToString());
                                }
                                if (SPdt.Rows[j]["Billtype"].ToString() == "PR")
                                {
                                    debit += Convert.ToDouble(SPdt.Rows[j]["Pqty"].ToString());
                                }
                            }
                            string pos = conn.ExecuteScalar("select ISNULL(SUM(Qty), 0) AS POSSale from BillPOSProductMaster where isactive=1 and ItemName='" + dt1.Rows[a]["rowitemname"].ToString() + "'");
                            debit += Convert.ToDouble(pos);
                            string production = conn.ExecuteScalar("select ISNULL(SUM(rawQty), 0) AS proSale from tblproductionrawmaterialmaster where isactive=1 and rawitemname='" + dt1.Rows[a]["rowitemname"].ToString() + "'");
                            debit += Convert.ToDouble(production);
                            Double qty = Convert.ToDouble(dt1.Rows[a]["rowqty"].ToString()) * Convert.ToDouble(lvmainitem.Items[i].SubItems[2].Text.Replace(",", ""));
                            li.SubItems.Add(Convert.ToString(qty));
                            string openingstockfromitem = conn.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + proid + "'");
                            Double finalqty = credit - debit + Convert.ToDouble(openingstockfromitem);
                            li.SubItems.Add(Convert.ToString(finalqty.ToString("N2")));
                            Double dec = finalqty - qty;
                            li.SubItems.Add(Convert.ToString(Math.Abs(dec).ToString("N2")));
                            li.SubItems.Add(dt1.Rows[a]["rowunit"].ToString());
                            li.SubItems.Add(proid);
                            #endregion
                        }
                        else
                        {
                            #region
                            for (int j = 0; j < SPdt.Rows.Count; j++)
                            {
                                if (SPdt.Rows[j]["Billtype"].ToString() == "S")
                                {
                                    debit += Convert.ToDouble(SPdt.Rows[j]["Pqty"].ToString());
                                }
                                if (SPdt.Rows[j]["Billtype"].ToString() == "SR")
                                {
                                    credit += Convert.ToDouble(SPdt.Rows[j]["Pqty"].ToString());
                                }
                                if (SPdt.Rows[j]["Billtype"].ToString() == "P")
                                {
                                    credit += Convert.ToDouble(SPdt.Rows[j]["Pqty"].ToString());
                                }
                                if (SPdt.Rows[j]["Billtype"].ToString() == "PR")
                                {
                                    debit += Convert.ToDouble(SPdt.Rows[j]["Pqty"].ToString());
                                }
                            }
                            string pos = conn.ExecuteScalar("select ISNULL(SUM(Qty), 0) AS POSSale from BillPOSProductMaster where isactive=1 and ItemName='" + dt1.Rows[a]["rowitemname"].ToString() + "'");
                            debit += Convert.ToDouble(pos);
                            string production = conn.ExecuteScalar("select ISNULL(SUM(rawQty), 0) AS proSale from tblproductionrawmaterialmaster where isactive=1 and rawitemname='" + dt1.Rows[a]["rowitemname"].ToString() + "'");
                            debit += Convert.ToDouble(production);
                            Double uq= Convert.ToDouble(dt1.Rows[a]["rowqty"].ToString())*Convert.ToDouble(lvmainitem.Items[i].SubItems[2].Text.Replace(",", ""));
                            Double qty = uq + Convert.ToDouble(updateqty);
                            //Double qty = Convert.ToDouble(dt1.Rows[a]["rowqty"].ToString()) * Convert.ToDouble(lvmainitem.Items[i].SubItems[2].Text.Replace(",", ""));

                            //li.SubItems.Add(Convert.ToString(qty));
                            string openingstockfromitem = conn.ExecuteScalar("select opstock from productpricemaster where isactive=1 and productid= '" + proid + "'");
                            Double finalqty = credit - debit + Convert.ToDouble(openingstockfromitem);
                          //  li.SubItems.Add(Convert.ToString(finalqty.ToString("N2")));
                            Double dec = finalqty - qty;
                            //li.SubItems.Add(Convert.ToString(Math.Abs(dec).ToString("N2")));
                          //  li.SubItems.Add(dt1.Rows[a]["rowunit"].ToString());
                        //    li.SubItems.Add(proid);
                            #endregion
                          //  lvaltitem.Items[rowid1].SubItems[0].Text = name;
                            lvaltitem.Items[rowid1].SubItems[1].Text = Convert.ToString(qty);
                           // lvaltitem.Items[rowid1].SubItems[2].Text = avail;
                            lvaltitem.Items[rowid1].SubItems[3].Text = Convert.ToString(Math.Abs(dec).ToString("N2"));
                        }
                    }

                }
            }
            catch
            {
            }

        }
        private void btnok_Click(object sender, EventArgs e)
        {
            binddata();

        }
        Int32 rowid = -1;
        Int32 rowid1 = -1;
        private void btnaddraw_Click(object sender, EventArgs e)
        {
            try
            {
                if (rowid >= 0)
                {
                    lvmainitem.Items[rowid].SubItems[0].Text = cmbitemtoproduce.Text;
                    lvmainitem.Items[rowid].SubItems[1].Text = cmbprocessname.Text;
                    lvmainitem.Items[rowid].SubItems[2].Text = txtpqty.Text;
                    lvmainitem.Items[rowid].SubItems[3].Text = txtpunit.Text;
                    btnaddraw.Text = "Add Item";
                    cmbprocessname.SelectedIndex = -1;
                    cmbitemtoproduce.SelectedIndex = -1;
                    txtpunit.Text = "";
                    txtpqty.Text = "";
                    cmbitemtoproduce.Focus();
                }
                else
                {
                    ListViewItem li;
                    li = lvmainitem.Items.Add(cmbitemtoproduce.Text);
                    li.SubItems.Add(cmbprocessname.Text);
                    li.SubItems.Add(txtpqty.Text);
                    li.SubItems.Add(txtpunit.Text);
                    cmbprocessname.SelectedIndex = -1;
                    cmbitemtoproduce.SelectedIndex = -1;
                    txtpunit.Text = "";
                    txtpqty.Text = "";
                    cmbitemtoproduce.Focus();
                    rowid = -1;
                }
            }
            catch
            {
            }
        }

        private void cmbprocessname_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool inList = false;
            for (int i = 0; i < cmbprocessname.Items.Count; i++)
            {
                s = cmbprocessname.GetItemText(cmbprocessname.Items[i]);
                if (s == cmbprocessname.Text)
                {
                    inList = true;
                    cmbprocessname.Text = s;
                    break;
                }
            }
            if (!inList)
            {
                cmbprocessname.Text = "";
            }
        }

        private void cmbitemtoproduce_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool inList = false;
            for (int i = 0; i < cmbitemtoproduce.Items.Count; i++)
            {
                s = cmbitemtoproduce.GetItemText(cmbitemtoproduce.Items[i]);
                if (s == cmbitemtoproduce.Text)
                {
                    inList = true;
                    cmbitemtoproduce.Text = s;
                    break;
                }
            }
            if (!inList)
            {
                cmbitemtoproduce.Text = "";
            }
        }
        public void Open()
        {
            if (lvmainitem.SelectedItems.Count > 0)
            {
                rowid = lvmainitem.FocusedItem.Index;
                cmbitemtoproduce.Text = lvmainitem.Items[lvmainitem.FocusedItem.Index].SubItems[0].Text;
                cmbprocessname.Text = lvmainitem.Items[lvmainitem.FocusedItem.Index].SubItems[1].Text;
                txtpqty.Text = lvmainitem.Items[lvmainitem.FocusedItem.Index].SubItems[2].Text;
                txtpunit.Text = lvmainitem.Items[lvmainitem.FocusedItem.Index].SubItems[3].Text;
                btnaddraw.Text = "Update";
                cmbitemtoproduce.Focus();
            }
        }
        private void lvmainitem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Delete Item?", "Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 == DialogResult.Yes)
                {
                    lvmainitem.Items[lvmainitem.FocusedItem.Index].Remove();
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                Open();
            }
        }

        private void lvmainitem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Open();
        }

        private void btnclose_Enter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_Leave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseEnter(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = false;
            btnclose.BackColor = Color.FromArgb(248, 152, 94);
            btnclose.ForeColor = Color.White;
        }

        private void btnclose_MouseLeave(object sender, EventArgs e)
        {
            btnclose.UseVisualStyleBackColor = true;
            btnclose.BackColor = Color.FromArgb(51, 153, 255);
            btnclose.ForeColor = Color.White;
        }

        private void btnprint_MouseEnter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_Enter(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = false;
            btnprint.BackColor = Color.FromArgb(176, 111, 193);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_Leave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
        }

        private void btnprint_MouseLeave(object sender, EventArgs e)
        {
            btnprint.UseVisualStyleBackColor = true;
            btnprint.BackColor = Color.FromArgb(51, 153, 255);
            btnprint.ForeColor = Color.White;
        }

        private void btnok_Enter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnok.ForeColor = System.Drawing.Color.White;
        }

        private void btnok_Leave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnok.ForeColor = System.Drawing.Color.White;
        }

        private void btnok_MouseEnter(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = false;
            btnok.BackColor = System.Drawing.Color.FromArgb(94, 191, 174);
            btnok.ForeColor = System.Drawing.Color.White;
        }

        private void btnok_MouseLeave(object sender, EventArgs e)
        {
            btnok.UseVisualStyleBackColor = true;
            btnok.BackColor = System.Drawing.Color.FromArgb(51, 153, 255);
            btnok.ForeColor = System.Drawing.Color.White;
        }
        Printing prn = new Printing();
        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr1 = MessageBox.Show("Do you want to Print Production Planning?", "Production", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dr1 == DialogResult.Yes)
                 {
                     prn.execute("delete from printing");
                     string status,status1,status2;
                     status = "PRODUCTION PLANNING REPORT";
                     status1 = "ITEMS TO PRODUCE";
                     status2 = "REQUIREMENT ANALYSIS";
                     string itemname="", process="", qtytopro="", unit="", itemsreq="", qtyreq="", qtyavai="", defi="", unit1="";
                     for (int i = 0; i < lvmainitem.Items.Count; i++)
                     {
                         itemname += Environment.NewLine + lvmainitem.Items[i].SubItems[0].Text;
                         process += Environment.NewLine + lvmainitem.Items[i].SubItems[1].Text;
                         qtytopro += Environment.NewLine + lvmainitem.Items[i].SubItems[2].Text;
                         unit += Environment.NewLine + lvmainitem.Items[i].SubItems[3].Text;
                     }
                     for (int i = 0; i < lvaltitem.Items.Count; i++)
                     {
                         itemsreq += Environment.NewLine + lvaltitem.Items[i].SubItems[0].Text;
                         qtyreq += Environment.NewLine + lvaltitem.Items[i].SubItems[1].Text;
                         qtyavai += Environment.NewLine + lvaltitem.Items[i].SubItems[2].Text;
                         defi += Environment.NewLine + lvaltitem.Items[i].SubItems[3].Text;
                         unit1 += Environment.NewLine + lvaltitem.Items[i].SubItems[4].Text;
                     }

                     DataTable dt1 = conn.getdataset("select * from company WHERE isactive=1 and CompanyID='" + Master.companyId + "' ");
                     string qry = "INSERT INTO Printing(T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,T13,T14,T15,T16,T17,T18,T19,T20,T21,T22,T23,T24,T25)VALUES";
                     qry += "('" + dt1.Rows[0]["CompanyName"].ToString() + "','" + dt1.Rows[0]["SubName"].ToString() + "','" + dt1.Rows[0]["Address"].ToString() + "','" + dt1.Rows[0]["Address2"].ToString() + "','" + dt1.Rows[0]["City"].ToString() + "','" + dt1.Rows[0]["State"].ToString() + "','" + dt1.Rows[0]["Country"].ToString() + "','" + dt1.Rows[0]["Phone"].ToString() + "','" + dt1.Rows[0]["Mobile"].ToString() + "','" + dt1.Rows[0]["Email"].ToString() + "','" + dt1.Rows[0]["CSTNo"].ToString() + "','" + dt1.Rows[0]["Website"].ToString() + "','" + status + "','" + status1 + "','" + status2 + "','" + itemname + "','" + process + "','" + qtytopro + "','" + unit + "','" + itemsreq + "','" + qtyreq + "','" + qtyavai + "','" + defi + "','" + unit1 + "','" + dt1.Rows[0]["WebSite"].ToString() + "')";
                     prn.execute(qry);

                     string reportName = "ProductionPlanning";
                     Print popup = new Print(reportName);
                     popup.ShowDialog();
                     popup.Dispose();
                 }
            }
            catch
            {
            }
        }

        private void btnaddraw_Enter(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = false;
            btnaddraw.BackColor = Color.FromArgb(9,106,3);
            btnaddraw.ForeColor = Color.White;
        }

        private void btnaddraw_Leave(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = true;
            btnaddraw.BackColor = Color.FromArgb(51, 153, 255);
            btnaddraw.ForeColor = Color.White;
        }

        private void btnaddraw_MouseEnter(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = false;
            btnaddraw.BackColor = Color.FromArgb(9,106,3);
            btnaddraw.ForeColor = Color.White;
        }

        private void btnaddraw_MouseLeave(object sender, EventArgs e)
        {
            btnaddraw.UseVisualStyleBackColor = true;
            btnaddraw.BackColor = Color.FromArgb(51, 153, 255);
            btnaddraw.ForeColor = Color.White;
        }

        private void btnreste_Enter(object sender, EventArgs e)
        {
            btnreste.UseVisualStyleBackColor = false;
            btnreste.BackColor = Color.FromArgb(250, 185, 34);
            btnreste.ForeColor = Color.White;
        }

        private void btnreste_Leave(object sender, EventArgs e)
        {
            btnreste.UseVisualStyleBackColor = true;
            btnreste.BackColor = Color.FromArgb(51, 153, 255);
            btnreste.ForeColor = Color.White;
        }

        private void btnreste_MouseEnter(object sender, EventArgs e)
        {
            btnreste.UseVisualStyleBackColor = false;
            btnreste.BackColor = Color.FromArgb(250, 185, 34);
            btnreste.ForeColor = Color.White;
        }

        private void btnreste_MouseLeave(object sender, EventArgs e)
        {
            btnreste.UseVisualStyleBackColor = true;
            btnreste.BackColor = Color.FromArgb(51, 153, 255);
            btnreste.ForeColor = Color.White;
        }
    }
}
