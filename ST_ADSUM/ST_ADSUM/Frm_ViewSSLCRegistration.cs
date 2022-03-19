using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_ViewSSLCRegistration : Form
    {
        String connectionStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;
        public static String stuId = "";
        public static int update = 0;

        public Frm_ViewSSLCRegistration()
        {
            InitializeComponent();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "SSLC Student report";
            printer.SubTitle = "";
            printer.PrintDataGridView(dataGridView1);
        }

        public void viewSearchSSLCRegistration(int flag)
        {
            SqlConnection con = new SqlConnection(connectionStr);
            try
            {
                SqlCommand cmd = new SqlCommand("prc_ViewSearchSSLCRegistration", con);
                if (flag == 1)
                {
                    cmd.Parameters.AddWithValue("@flag", 1);
                    cmd.Parameters.AddWithValue("@search", txtbx_year.Text.Trim());
                    cmd.Parameters.AddWithValue("@para", "");
                }
                else if (flag == 2)
                {
                    cmd.Parameters.AddWithValue("@flag", 1);
                    cmd.Parameters.AddWithValue("@search", txtbx_admission_satsNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@para", "");
                }
                else if (flag == 3)
                {
                    cmd.Parameters.AddWithValue("@flag", 3);
                    cmd.Parameters.AddWithValue("@search",txtbx_SSLCYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@para", "");
                }
                else if (flag == 4)
                {
                    cmd.Parameters.AddWithValue("@flag", 4);
                    cmd.Parameters.AddWithValue("@search", cmbbx_mediumOfInst.Text);
                    cmd.Parameters.AddWithValue("@para", "");
                }
                else if (flag == 5)
                {
                    cmd.Parameters.AddWithValue("@flag", 5);
                    cmd.Parameters.AddWithValue("@search", cmbbx_mediumOfInst.Text);
                    cmd.Parameters.AddWithValue("@para", txtbx_SSLCYear.Text);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adt.Fill(dt);

                    dataGridView1.DataSource = null;

                    dataGridView1.AutoGenerateColumns = false;

                    dataGridView1.ColumnCount = 13;

                    dataGridView1.Columns[1].HeaderText = "Admission No";
                    dataGridView1.Columns[1].Name = "roa_StuAdmissionNo";
                    dataGridView1.Columns[1].DataPropertyName = "roa_StuAdmissionNo";
                    dataGridView1.Columns[1].ReadOnly = true;

                    dataGridView1.Columns[2].HeaderText = "SATS No";
                    dataGridView1.Columns[2].Name = "roa_StuSatsNo";
                    dataGridView1.Columns[2].DataPropertyName = "roa_StuSatsNo";
                    dataGridView1.Columns[2].ReadOnly = true;

                    dataGridView1.Columns[3].HeaderText = "Student Name";
                    dataGridView1.Columns[3].Name = "stuName";
                    dataGridView1.Columns[3].DataPropertyName = "stuName";
                    dataGridView1.Columns[3].ReadOnly = true;

                    dataGridView1.Columns[4].HeaderText = "Gender";
                    dataGridView1.Columns[4].Name = "roa_Gender";
                    dataGridView1.Columns[4].DataPropertyName = "roa_Gender";
                    dataGridView1.Columns[4].ReadOnly = true;
                    dataGridView1.Columns[4].Width = 70;

                    dataGridView1.Columns[5].HeaderText = "Date of Birth";
                    dataGridView1.Columns[5].Name = "roa_DOB";
                    dataGridView1.Columns[5].DataPropertyName = "roa_DOB";
                    dataGridView1.Columns[5].ReadOnly = true;
                    dataGridView1.Columns[5].Width = 70;

                    dataGridView1.Columns[6].HeaderText = "Father Name";
                    dataGridView1.Columns[6].Name = "fatherName";
                    dataGridView1.Columns[6].DataPropertyName = "fatherName";
                    dataGridView1.Columns[6].ReadOnly = true;

                    dataGridView1.Columns[7].HeaderText = "Adhaar No.";
                    dataGridView1.Columns[7].Name = "sr_StuAdhaarNo";
                    dataGridView1.Columns[7].DataPropertyName = "sr_StuAdhaarNo";
                    dataGridView1.Columns[7].ReadOnly = true;

                    dataGridView1.Columns[8].HeaderText = "Phone No.";
                    dataGridView1.Columns[8].Name = "sr_StuFatherPhoneNo";
                    dataGridView1.Columns[8].DataPropertyName = "sr_StuFatherPhoneNo";
                    dataGridView1.Columns[8].ReadOnly = true;
                    dataGridView1.Columns[8].Width = 110;

                    dataGridView1.Columns[9].HeaderText = "Income No.";
                    dataGridView1.Columns[9].Name = "sr_IncomeNo";
                    dataGridView1.Columns[9].DataPropertyName = "sr_IncomeNo";
                    dataGridView1.Columns[9].ReadOnly = true;
                    dataGridView1.Columns[9].Visible= false;

                    dataGridView1.Columns[10].HeaderText = "Medium of Inst.";
                    dataGridView1.Columns[10].Name = "sr_MediumOfInstruction";
                    dataGridView1.Columns[10].DataPropertyName = "sr_MediumOfInstruction";
                    dataGridView1.Columns[10].ReadOnly = true;

                    dataGridView1.Columns[11].DataPropertyName = "sr_StuId";
                    dataGridView1.Columns[11].Visible = false;

                    dataGridView1.Columns[12].DataPropertyName = "sr_roa_Id";
                    dataGridView1.Columns[12].Visible = false;

                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    dataGridView1.Columns.Add(btn);                    
                    btn.HeaderText = "Action";
                    btn.Text = "View";
                    btn.Name = "btnGrid_Edit";
                    btn.UseColumnTextForButtonValue = true;
                    dataGridView1.DataSource = dt;                    
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong" + ex);
            }
            finally { con.Close(); }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            viewSearchSSLCRegistration(1);
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            txtbx_year.Text = "";
            txtbx_admission_satsNo.Text = "";
            txtbx_SSLCYear.Text = "";
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void txtbx_admission_satsNo_TextChanged(object sender, EventArgs e)
        {
            viewSearchSSLCRegistration(2);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 13)  //last column view button
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                Frm_ViewSSLCRegistration.update = 1;

                //static variable that stores the subject id
                Frm_ViewSSLCRegistration.stuId = row.Cells[11].Value.ToString();

                Frm_SSLCRegistration fsr = new Frm_SSLCRegistration();
                fsr.MdiParent = this.MdiParent;
                fsr.Show();         
            }
        }

        private void txtbx_SSLCYear_TextChanged(object sender, EventArgs e)
        {
            viewSearchSSLCRegistration(3);
        }

        private void cmbbx_mediumOfInst_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbx_SSLCYear.Text = "";
            viewSearchSSLCRegistration(4);
        }

        private void btn_grp_search_Click(object sender, EventArgs e)
        {
            viewSearchSSLCRegistration(5);
        }
    }
}
