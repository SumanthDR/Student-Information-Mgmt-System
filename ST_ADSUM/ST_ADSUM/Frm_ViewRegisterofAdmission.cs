using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_ViewRegisterofAdmission : Form
    {
        String connectionStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;

        public static String stuId = "";
        public static int update = 0;

        public Frm_ViewRegisterofAdmission()
        {
            InitializeComponent();
        }

        public void viewSearchRegisterofAdmission(int flag)
        {
            SqlConnection con = new SqlConnection(connectionStr);
            try
            {
                SqlCommand cmd = new SqlCommand("Prc_ViewSearchRegisterofAdmission", con);
                if (flag == 1)
                {
                    cmd.Parameters.AddWithValue("@flag", 1);
                    cmd.Parameters.AddWithValue("@search", "");
                    cmd.Parameters.AddWithValue("@dob", datePicker_DOB.Value.Date);
                    cmd.Parameters.AddWithValue("@para", "");
                    cmd.Parameters.AddWithValue("@para1", "");
                }
                else if (flag == 2)
                {
                    cmd.Parameters.AddWithValue("@flag", 2);
                    cmd.Parameters.AddWithValue("@search", txtbx_searchsub.Text);
                    cmd.Parameters.AddWithValue("@dob", datePicker_DOB.Value.Date);
                    cmd.Parameters.AddWithValue("@para", "");
                    cmd.Parameters.AddWithValue("@para1", "");
                }
                else if (flag == 4)
                {
                    cmd.Parameters.AddWithValue("@flag", 4);
                    cmd.Parameters.AddWithValue("@search", "");
                    cmd.Parameters.AddWithValue("@dob", datePicker_DOB.Value.Date);
                    cmd.Parameters.AddWithValue("@para", "");
                    cmd.Parameters.AddWithValue("@para1", "");
                }
                else if (flag == 5)
                {
                    cmd.Parameters.AddWithValue("@flag", 5);
                    cmd.Parameters.AddWithValue("@search", txtbx_year.Text);
                    cmd.Parameters.AddWithValue("@dob", "");
                    cmd.Parameters.AddWithValue("@para", "");
                    cmd.Parameters.AddWithValue("@para1", "");
                }
                else if (flag == 6)
                {
                    cmd.Parameters.AddWithValue("@flag", 6);
                    cmd.Parameters.AddWithValue("@search", txtbx_year.Text);
                    cmd.Parameters.AddWithValue("@dob", "");
                    cmd.Parameters.AddWithValue("@para", cmbbx_std.Text);
                    cmd.Parameters.AddWithValue("@para1", cmbbx_sec.Text);
                }
                else if (flag == 7)
                {
                    cmd.Parameters.AddWithValue("@flag", 7);
                    cmd.Parameters.AddWithValue("@search", txtbx_year.Text);
                    cmd.Parameters.AddWithValue("@dob", "");
                    cmd.Parameters.AddWithValue("@para", cmbbx_std.Text);
                    cmd.Parameters.AddWithValue("@para1", cmbbx_sec.Text);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adt.Fill(dt);

                    dataGridView1.DataSource = null;

                    dataGridView1.AutoGenerateColumns = false;

                    dataGridView1.ColumnCount = 11;

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
                    dataGridView1.Columns[4].Width = 70;
                    dataGridView1.Columns[4].ReadOnly = true;

                    dataGridView1.Columns[5].HeaderText = "Date of Birth";
                    dataGridView1.Columns[5].Name = "roa_DOB";
                    dataGridView1.Columns[5].DataPropertyName = "roa_DOB";
                    dataGridView1.Columns[5].ReadOnly = true;

                    dataGridView1.Columns[6].HeaderText = "Father Name";
                    dataGridView1.Columns[6].Name = "fatherName";
                    dataGridView1.Columns[6].DataPropertyName = "fatherName";
                    dataGridView1.Columns[6].ReadOnly = true;                                      

                    dataGridView1.Columns[7].HeaderText = "Std Admitted";
                    dataGridView1.Columns[7].Name = "roa_StdAdmitted";
                    dataGridView1.Columns[7].DataPropertyName = "roa_StdAdmitted";
                    dataGridView1.Columns[7].Width = 70;
                    dataGridView1.Columns[7].ReadOnly = true;

                    dataGridView1.Columns[8].HeaderText = "Section";
                    dataGridView1.Columns[8].Name = "roa_StdSection";
                    dataGridView1.Columns[8].DataPropertyName = "roa_StdSection";
                    dataGridView1.Columns[8].Width = 70;
                    dataGridView1.Columns[8].ReadOnly = true;

                    dataGridView1.Columns[9].HeaderText = "Date of Admission";
                    dataGridView1.Columns[9].Name = "roa_StuDOA";
                    dataGridView1.Columns[9].DataPropertyName = "roa_StuDOA";
                    dataGridView1.Columns[9].ReadOnly = true;

                    dataGridView1.Columns[10].DataPropertyName = "roa_StuId";
                    dataGridView1.Columns[10].Visible = false;

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

        private void Frm_ViewRegisterofAdmission_Load(object sender, EventArgs e)
        {
            viewSearchRegisterofAdmission(1);
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "LIST OF NEW ADMISSION STUDENTS";
            printer.SubTitle = "";
            printer.PrintDataGridView(dataGridView1);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11)  //last column view button
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                Frm_ViewRegisterofAdmission.update = 1;

                //static variable that stores the subject id
                Frm_ViewRegisterofAdmission.stuId = row.Cells[10].Value.ToString();

                Frm_RegisterofAdmission fra = new Frm_RegisterofAdmission();
                fra.MdiParent = this.MdiParent;
                fra.Show();
                // this.Close();
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            viewSearchRegisterofAdmission(1);
        }

        private void datePicker_DOB_ValueChanged(object sender, EventArgs e)
        {
            viewSearchRegisterofAdmission(4);
        }

        private void txtbx_searchsub_TextChanged(object sender, EventArgs e)
        {
            viewSearchRegisterofAdmission(2);
        }

        private void txtbx_year_TextChanged(object sender, EventArgs e)
        {
            viewSearchRegisterofAdmission(5);
        }

        private void cmbbx_std_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtbx_year.Text != "")
                viewSearchRegisterofAdmission(6);
            else
                MessageBox.Show("Enter the Year");
        }

        private void cmbbx_sec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((txtbx_year.Text != "") && (cmbbx_sec.Text != ""))
                viewSearchRegisterofAdmission(7);
            else
                MessageBox.Show("Enter Year and Select Std.");
        }
    }
}
