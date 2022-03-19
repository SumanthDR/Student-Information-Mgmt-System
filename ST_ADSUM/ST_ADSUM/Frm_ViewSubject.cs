using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_ViewSubject : Form
    {

        String connectionStr = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;

        public static String subId;
        public static int update = 0;


        public Frm_ViewSubject()
        {
            InitializeComponent();
        }

        //for default row numbers
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        //To extract data from database to datagrid view and search on name and subject type
        public void viewSearchSubject(int flag)
        {
            SqlConnection con = new SqlConnection(connectionStr);
            try
            {
                SqlCommand cmd = new SqlCommand("Prc_ViewSearchSubject", con);
                if (flag == 1)
                {
                    cmd.Parameters.AddWithValue("@flag", 1);
                    cmd.Parameters.AddWithValue("@search", "");
                }
                else if (flag == 2)
                {
                    cmd.Parameters.AddWithValue("@flag", 2);
                    cmd.Parameters.AddWithValue("@search", txtbx_searchsub.Text);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adt.Fill(dt);

                    dataGridView1.DataSource = null;

                    dataGridView1.AutoGenerateColumns = false;

                    dataGridView1.ColumnCount = 5;

                    dataGridView1.Columns[1].HeaderText = "Subject Code";
                    dataGridView1.Columns[1].Name = "sub_code";
                    dataGridView1.Columns[1].DataPropertyName = "sub_Code";
                    dataGridView1.Columns[1].ReadOnly = true;

                    dataGridView1.Columns[2].HeaderText = "Subject Name";
                    dataGridView1.Columns[2].Name = "sub_name";
                    dataGridView1.Columns[2].DataPropertyName = "sub_Name";
                    dataGridView1.Columns[2].ReadOnly = true;

                    dataGridView1.Columns[3].HeaderText = "Subject Type";
                    dataGridView1.Columns[3].Name = "sub_type";
                    dataGridView1.Columns[3].DataPropertyName = "sub_type";
                    dataGridView1.Columns[3].ReadOnly = true;

                    dataGridView1.Columns[4].DataPropertyName = "sub_Id";
                    dataGridView1.Columns[4].Visible = false;

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

        private void Frm_ViewSubject_Load(object sender, EventArgs e)
        {
            viewSearchSubject(1);  //1 is passed as a parameter for refresh and extracting all the data
        }

        private void txtbx_searchsub_TextChanged(object sender, EventArgs e)
        {
            viewSearchSubject(2);  //2 is passed for searching purpose.
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            txtbx_searchsub.Text = "";
            viewSearchSubject(1); //1 is passed as a parameter for refresh and extracting all the data
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)  //last column view button
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                Frm_ViewSubject.update = 1;

                //static variable that stores the subject id
                Frm_ViewSubject.subId = row.Cells[4].Value.ToString();

                Frm_Subject fs = new Frm_Subject();
                fs.MdiParent = this.MdiParent;
                fs.Show();
                // this.Close();
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "LIST OF SUBJECTS OFFERED";
            printer.SubTitle = " ";
            printer.PrintDataGridView(dataGridView1);
        }
    }
}
