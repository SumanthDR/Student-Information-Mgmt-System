using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_Subject : Form
    {
        public Frm_Subject()
        {
            InitializeComponent();
        }
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;
        public int flag = 0;

        //Checks the fileds are not empty
        public void ValidateValues()
        {
            flag = 0;
            String name = txtbx_sub_name.Text.Trim();
            if (cmb_sub_type.Text == "")
            {
                errorProvider_type.SetError(cmb_sub_type, "Select subject type");
                label_status.Text = "Select subject type";
                flag = 1;
            }
            if (name == "")
            {
                errorProvider_name.SetError(txtbx_sub_name, "Enter the subject name");
                label_status.Text = "Enter the subject name";
                flag = 1;
            }
        }
        public void SaveUpdateDeleteSubject(int flags)
        {
            ValidateValues();//Calling Function to Check the fileds are not empty
            if (flag == 0) //if all the text boxes are validated correctly then flag remains zero.
            {
                SqlConnection con = new SqlConnection(connectionString);
                try
                {
                    SqlCommand cmd = new SqlCommand("prc_SubjectInsert", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@flag", flags);
                    cmd.Parameters.AddWithValue("@subId", Frm_ViewSubject.subId);


                    cmd.Parameters.AddWithValue("@code", txtbx_sub_code.Text.TrimStart().TrimEnd());
                    cmd.Parameters.AddWithValue("@name", txtbx_sub_name.Text.TrimStart().TrimEnd());
                    cmd.Parameters.AddWithValue("@description", txtbx_sub_desc.Text.TrimStart().TrimEnd());
                    cmd.Parameters.AddWithValue("@creationDate", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@isActive", 1);
                    cmd.Parameters.AddWithValue("@subType", cmb_sub_type.Text);
                    cmd.Parameters.Add("@ERROR", SqlDbType.Char, 500);
                    cmd.Parameters["@ERROR"].Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    label_status.Text = (string)cmd.Parameters["@ERROR"].Value;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Something went wrong ! " + ex);
                }
                finally { con.Close(); }
                ClearTextBoxes();
            }
        }

        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
            label_status.Text = "...";
        }

        //this function is to save the record.
        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveUpdateDeleteSubject(1);
        }
        //event to disable key input
        private void cmb_sub_type_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //
        private void Frm_Subject_Load(object sender, EventArgs e)
        {
            if (Frm_ViewSubject.update == 1)
            {
                btn_edit.Visible = true;

                txtbx_sub_code.Enabled = false;
                txtbx_sub_name.Enabled = false;
                txtbx_sub_desc.Enabled = false;
                cmb_sub_type.Enabled = false;

                btn_cancel.Visible = false;
                btn_reset.Visible = false;
                btn_save.Visible = false;

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("Prc_ViewSearchSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@flag", 3);
                cmd.Parameters.AddWithValue("@search", Frm_ViewSubject.subId);
                con.Open();
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtbx_sub_code.Text = dr["sub_Code"].ToString();
                        txtbx_sub_name.Text = dr["sub_Name"].ToString();
                        cmb_sub_type.Text = dr["sub_type"].ToString();
                        txtbx_sub_desc.Text = dr["sub_Description"].ToString();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Something went wrong " + ex);
                }
                finally { con.Close(); }
            }
            Frm_ViewSubject.update = 0;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            btn_edit.Visible = false;

            txtbx_sub_code.Enabled = true;
            txtbx_sub_name.Enabled = true;
            txtbx_sub_desc.Enabled = true;
            cmb_sub_type.Enabled = true;

            btn_cancel.Visible = true;
            btn_delete.Visible = true;
            btn_update.Visible = true;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            SaveUpdateDeleteSubject(2);
            this.Hide();
        }

        private void txtbx_sub_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtbx_sub_code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            SaveUpdateDeleteSubject(3);
            this.Hide();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Are you sure.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }
    }
}
