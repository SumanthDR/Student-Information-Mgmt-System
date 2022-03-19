using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_RegisterofAdmission : Form
    {
        public Frm_RegisterofAdmission()
        {
            InitializeComponent();
        }

        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;

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

            createAdmissionNo();
        }

        public void createAdmissionNo()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd1 = new SqlCommand("Prc_AdmissionNoCreation", con))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader dr = cmd1.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                txtbx_admno.Text = (dr["AdmissionNo"].ToString());
                            }
                        }
                        con.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong " + ex);
            }
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        public int flag = 0;

        //This function is written to inform user to fill the text boxes
        public void ValidateValuesText(TextBox txtbx, ErrorProvider err)
        {
            if (txtbx.Text.Trim() == "")
            {
                err.SetError(txtbx, "This Field is complusory");
                label_status.Text = "ERROR !, Please check the Red Icon";
                flag = 1;
            }
            else
                err.SetError(txtbx, "");
        }

        //This function is written to inform user to fill the combo boxes
        public void ValidateValuesComboBox(ComboBox cmb, ErrorProvider err)
        {
            if (cmb.Text == "")
            {
                err.SetError(cmb, "This Field is complusory");
                label_status.Text = "ERROR !, Please check the Red Icon";
                flag = 1;
            }
            else            
                err.SetError(cmb, "");            
        }


        public void ValidateValues()
        {

            ValidateValuesText(txtbx_satsno, errorProvider_saats);
            ValidateValuesText(txtbx_fname, errorProvider_stu_name);
            ValidateValuesText(txtbx_dadfname, errorProvider_dad_name);
            ValidateValuesText(txtbx_foccu, errorProvider_dad_occu);
            ValidateValuesText(txtbx_momfname, errorProvider_mom_name);
            ValidateValuesText(txtbx_moccu, errorProvider_mom_occu);
            ValidateValuesText(txtbx_income, errorProvider_income);
            ValidateValuesText(txtbx_dependants, errorProvider_dependants);
            ValidateValuesText(txtbx_caste, errorProvider_caste);
            ValidateValuesText(txtbx_address, errorProvider_address);
            ValidateValuesText(txtbx_lschool, errorProvider_last_school);
            ValidateValuesText(txtbx_ptcno, errorProvider_ptc);

            ValidateValuesComboBox(cmbbx_gender, errorProvider_gender);
            ValidateValuesComboBox(cmbbx_lstd, errorProvider_last_std);
            ValidateValuesComboBox(cmbbx_mtongue, errorProvider_mtongue);
            ValidateValuesComboBox(cmbbx_nationality, errorProvider_nationality);
            ValidateValuesComboBox(cmbbx_religion, errorProvider_religion);
            ValidateValuesComboBox(cmbbx_std_admitted, errorProvider_std_admitted);
            ValidateValuesComboBox(cmbbx_std_section, errorProvider_section);

        }

        public void SaveUpdateData(int flags)
        {
            flag = 0;
            ValidateValues();
            ValidateDates();
            if (txtbx_satsno.Text.Length != 9)
            {
                MessageBox.Show("SAATS number should be 9 characters");
            }
            else if (flag == 0)
            {
                SqlConnection con = new SqlConnection(connectionString);
                try
                {
                    SqlCommand cmd = new SqlCommand("prcRegisterOfAdmissionInsert", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flags", flags);
                    cmd.Parameters.AddWithValue("@stuId", Frm_ViewRegisterofAdmission.stuId);
                    cmd.Parameters.AddWithValue("@admissionNo", txtbx_admno.Text);
                    cmd.Parameters.AddWithValue("@satsNo", txtbx_satsno.Text);
                    cmd.Parameters.AddWithValue("@stuFirstName", txtbx_fname.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuSecondName", txtbx_mname.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuThirdName", txtbx_lname.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuGender", cmbbx_gender.Text);
                    cmd.Parameters.AddWithValue("@stuDOB", date_dob.Value.Date);
                    cmd.Parameters.AddWithValue("@stuFatherFirstName", txtbx_dadfname.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuFatherSecondName", txtbx_dadmname.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuFatherThirdName", txtbx_dadlname.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuFatherOccupation", txtbx_foccu.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuMotherFirstName", txtbx_momfname.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuMotherSecondName", txtbx_mommname.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuMotherThirdName", txtbx_momlname.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuMotherOccupation", txtbx_moccu.Text.Trim());
                    cmd.Parameters.AddWithValue("@annualIncome", txtbx_income.Text.Trim());
                    cmd.Parameters.AddWithValue("@noOfDependants", txtbx_dependants.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuNationality", cmbbx_nationality.Text);
                    cmd.Parameters.AddWithValue("@stuReligion", cmbbx_religion.Text);
                    cmd.Parameters.AddWithValue("@stuCaste", txtbx_caste.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuMotherTounge", cmbbx_mtongue.Text);
                    cmd.Parameters.AddWithValue("@stuPermanentAddress", txtbx_address.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuLastSchoolAttended", txtbx_lschool.Text.Trim());
                    cmd.Parameters.AddWithValue("@stuLastStdAdmitted", cmbbx_lstd.Text);
                    cmd.Parameters.AddWithValue("@stuPreviousTCNo", txtbx_ptcno.Text);
                    cmd.Parameters.AddWithValue("@stuPreviousTCDate", date_ptc.Value.Date);
                    cmd.Parameters.AddWithValue("@stuStdAdmitted", cmbbx_std_admitted.Text);
                    cmd.Parameters.AddWithValue("@stuStdSection", cmbbx_std_section.Text);
                    cmd.Parameters.AddWithValue("@stuDOA", date_doa.Value.Date);
                    cmd.Parameters.AddWithValue("@isActive", 1);
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
                createAdmissionNo();

                txtbx_moccu.Text = "HOUSE WIFE";
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveUpdateData(1);
        }

        //This function is for the automatic selection of the standard admitted by using last standard attended
        private void cmbbx_lstd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbbx_lstd.Text == "7")
                cmbbx_std_admitted.Text = "8";
            else if (cmbbx_lstd.Text == "8")
                cmbbx_std_admitted.Text = "9";
            else if (cmbbx_lstd.Text == "9")
                cmbbx_std_admitted.Text = "10";
        }

        private void cmbbx_lstd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbbx_std_section_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbbx_gender_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbbx_nationality_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbbx_religion_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbbx_mtongue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbbx_std_admitted_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //this is to get only alphabets in the textbox
        private void txtbx_fname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }
        }



        //this is for getting numeric inputs only
        private void txtbx_satsno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Are you sure.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void Frm_RegisterofAdmission_Load(object sender, EventArgs e)
        {
            if (Frm_ViewRegisterofAdmission.update == 1)
            {
                txtbx_satsno.Enabled = false;

                txtbx_fname.Enabled = false;
                txtbx_mname.Enabled = false;
                txtbx_lname.Enabled = false;

                cmbbx_gender.Enabled = false;
                date_dob.Enabled = false;

                txtbx_dadfname.Enabled = false;
                txtbx_dadmname.Enabled = false;
                txtbx_dadlname.Enabled = false;

                txtbx_foccu.Enabled = false;

                txtbx_momfname.Enabled = false;
                txtbx_mommname.Enabled = false;
                txtbx_momlname.Enabled = false;

                txtbx_moccu.Enabled = false;

                txtbx_income.Enabled = false;
                txtbx_dependants.Enabled = false;

                cmbbx_nationality.Enabled = false;

                cmbbx_religion.Enabled = false;
                txtbx_caste.Enabled = false;

                cmbbx_mtongue.Enabled = false;

                txtbx_address.Enabled = false;

                txtbx_lschool.Enabled = false;

                cmbbx_lstd.Enabled = false;

                txtbx_ptcno.Enabled = false;
                date_ptc.Enabled = false;

                cmbbx_std_admitted.Enabled = false;
                cmbbx_std_section.Enabled = false;
                date_doa.Enabled = false;

                btn_edit.Visible = true;

                btn_save.Visible = false;
                btn_cancel.Visible = false;
                btn_reset.Visible = false;

                btn_update.Visible = false;
                btn_delete.Visible = false;

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("Prc_ViewSearchRegisterofAdmission", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@flag", 3);
                cmd.Parameters.AddWithValue("@search", Frm_ViewRegisterofAdmission.stuId);
                cmd.Parameters.AddWithValue("@dob", "");
                cmd.Parameters.AddWithValue("@para", "");
                cmd.Parameters.AddWithValue("@para1", "");
                con.Open();
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        txtbx_admno.Text = dr["roa_StuAdmissionNo"].ToString();
                        txtbx_satsno.Text = dr["roa_StuSatsNo"].ToString();
                        txtbx_fname.Text = dr["roa_StuFirstName"].ToString();
                        txtbx_mname.Text = dr["roa_StuSecondName"].ToString();
                        txtbx_lname.Text = dr["roa_StuThirdName"].ToString();

                        cmbbx_gender.Text = dr["roa_Gender"].ToString();
                        date_dob.Text = dr["roa_DOB"].ToString();

                        txtbx_dadfname.Text = dr["roa_FatherFirstName"].ToString();
                        txtbx_dadmname.Text = dr["roa_FatherSecondName"].ToString();
                        txtbx_dadlname.Text = dr["roa_FatherThirdName"].ToString();

                        txtbx_foccu.Text = dr["roa_FatherOccupation"].ToString();

                        txtbx_momfname.Text = dr["roa_MotherFirstName"].ToString();
                        txtbx_mommname.Text = dr["roa_MotherSecondName"].ToString();
                        txtbx_momlname.Text = dr["roa_MotherThirdName"].ToString();

                        txtbx_moccu.Text = dr["roa_MotherOccupation"].ToString();

                        txtbx_income.Text = dr["roa_AnnualIncome"].ToString();
                        txtbx_dependants.Text = dr["roa_NoOfDependants"].ToString();

                        cmbbx_nationality.Text = dr["roa_Nationality"].ToString();

                        cmbbx_religion.Text = dr["roa_Religion"].ToString();
                        txtbx_caste.Text = dr["roa_Caste"].ToString();

                        cmbbx_mtongue.Text = dr["mothertounge"].ToString();

                        txtbx_address.Text = dr["roa_PermanentAddress"].ToString();

                        txtbx_lschool.Text = dr["roa_LastSchoolAttended"].ToString();

                        cmbbx_lstd.Text = dr["roa_LastStdAdmitted"].ToString();

                        txtbx_ptcno.Text = dr["roa_PreviousTCNo"].ToString();
                        date_ptc.Text = dr["roa_PreviousTCDate"].ToString();

                        cmbbx_std_admitted.Text = dr["roa_StdAdmitted"].ToString();
                        cmbbx_std_section.Text = dr["roa_StdSection"].ToString();
                        date_doa.Text = dr["roa_StuDOA"].ToString();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Something went wrong " + ex);
                }
                finally { con.Close(); }

                Frm_ViewRegisterofAdmission.update = 0;
            }
            else
            {
                createAdmissionNo();

                btn_update.Visible = false;
                btn_delete.Visible = false;

            }
        }

        //function to accept only alphabets
        private void txtbx_moccu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }
        }

        //function to accept only alphabets
        private void txtbx_moccu_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }
        }

        //function to accept alpha numeric values
        private void txtbx_lschool_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }

        }

        private void txtbx_ptcno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // This is an Event printPage
        // Below method is created when you "select -> printDocument1 -> Events -> PrintPage(Double Click)"
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Print Labels

            string heading = "Register of Admission";

            e.Graphics.DrawString(heading, new Font("Arial", 25, FontStyle.Bold), Brushes.Black, 270, 30);
            e.Graphics.DrawString(lbl_admissionNo.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 160);
            e.Graphics.DrawString(lbl_SATSNo.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 190);
            e.Graphics.DrawString(lbl_stuName.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 220);
            e.Graphics.DrawString(lbl_Gender.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 250);
            e.Graphics.DrawString(lbl_DOB.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 280);
            e.Graphics.DrawString(lbl_FName.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 310);
            e.Graphics.DrawString(lbl_FOccupation.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 340);
            e.Graphics.DrawString(lbl_MName.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 370);
            e.Graphics.DrawString(lbl_MOccupation.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 400);
            e.Graphics.DrawString(lbl_AnnualIncome.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 430);
            e.Graphics.DrawString(lbl_NoOfDependents.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 460);
            e.Graphics.DrawString(lbl_Natioanlity.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 490);
            e.Graphics.DrawString(lbl_Religion.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 520);
            e.Graphics.DrawString(lbl_Caste.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 550);
            e.Graphics.DrawString(lbl_MTounge.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 580);
            e.Graphics.DrawString(lbl_permanentAddress.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 610);
            e.Graphics.DrawString(lbl_lastSchoolAttended.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 670);
            e.Graphics.DrawString(lbl_StdAttended.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 700);
            e.Graphics.DrawString(lbl_previousTCNo.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 730);
            e.Graphics.DrawString(lbl_previousTCDate.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 760);
            e.Graphics.DrawString(lbl_stdAdmitted.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 790);
            e.Graphics.DrawString(lbl_stdSection.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 820);
            e.Graphics.DrawString(lbl_DOA.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 850);


            //Print Values in Textbox

            e.Graphics.DrawString(txtbx_admno.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 160);
            e.Graphics.DrawString(txtbx_satsno.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 190);
            e.Graphics.DrawString(txtbx_fname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 220);
            e.Graphics.DrawString(txtbx_mname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 520, 220);
            e.Graphics.DrawString(txtbx_lname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 640, 220);
            e.Graphics.DrawString(txtbx_fname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 220);
            e.Graphics.DrawString(cmbbx_gender.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 250);
            e.Graphics.DrawString(date_dob.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 280);
            e.Graphics.DrawString(txtbx_dadfname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 310);
            e.Graphics.DrawString(txtbx_dadmname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 520, 310);
            e.Graphics.DrawString(txtbx_dadlname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 640, 310);
            e.Graphics.DrawString(txtbx_foccu.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 340);
            e.Graphics.DrawString(txtbx_momfname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 370);
            e.Graphics.DrawString(txtbx_mommname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 520, 370);
            e.Graphics.DrawString(txtbx_momlname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 640, 370);
            e.Graphics.DrawString(txtbx_moccu.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 400);
            e.Graphics.DrawString(txtbx_income.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 430);
            e.Graphics.DrawString(txtbx_dependants.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 460);
            e.Graphics.DrawString(cmbbx_nationality.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 490);
            e.Graphics.DrawString(cmbbx_religion.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 520);
            e.Graphics.DrawString(txtbx_caste.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 550);
            e.Graphics.DrawString(cmbbx_mtongue.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 580);
            e.Graphics.DrawString(txtbx_address.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 610);
            e.Graphics.DrawString(txtbx_lschool.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 670);
            e.Graphics.DrawString(cmbbx_lstd.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 700);
            e.Graphics.DrawString(txtbx_ptcno.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 730);
            e.Graphics.DrawString(date_ptc.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 760);
            e.Graphics.DrawString(cmbbx_std_admitted.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 790);
            e.Graphics.DrawString(cmbbx_std_section.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 820);
            e.Graphics.DrawString(date_doa.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 850);
        }

        private void txtbx_satsno_TextChanged(object sender, EventArgs e)
        {
            if (txtbx_satsno.Text != "")
                label_status.Text = "...";
        }

        private void toolStripPrint_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        public void ValidateDates()
        {
            int thisYear = date_doa.Value.Year;
            int yourYear = date_dob.Value.Year;
            int ptcYear = date_ptc.Value.Year;
            if (thisYear - yourYear < 12)
            {
                MessageBox.Show("Age must be 12 and above!");
                flag = 1;
                label_status.Text = "Check Date of birth";
            }            
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            btn_edit.Visible = false;

            btn_update.Visible = true;
            btn_cancel.Visible = true;
            btn_delete.Visible = true;

            txtbx_satsno.Enabled = true;

            txtbx_fname.Enabled = true;
            txtbx_mname.Enabled = true;
            txtbx_lname.Enabled = true;

            cmbbx_gender.Enabled = true;
            date_dob.Enabled = true;

            txtbx_dadfname.Enabled = true;
            txtbx_dadmname.Enabled = true;
            txtbx_dadlname.Enabled = true;

            txtbx_foccu.Enabled = true;

            txtbx_momfname.Enabled = true;
            txtbx_mommname.Enabled = true;
            txtbx_momlname.Enabled = true;

            txtbx_moccu.Enabled = true;

            txtbx_income.Enabled = true;
            txtbx_dependants.Enabled = true;

            cmbbx_nationality.Enabled = true;

            cmbbx_religion.Enabled = true;
            txtbx_caste.Enabled = true;

            cmbbx_mtongue.Enabled = true;

            txtbx_address.Enabled = true;

            txtbx_lschool.Enabled = true;

            cmbbx_lstd.Enabled = true;

            txtbx_ptcno.Enabled = true;
            date_ptc.Enabled = true;

            cmbbx_std_admitted.Enabled = true;
            cmbbx_std_section.Enabled = true;
            date_doa.Enabled = true;

            btn_update.Visible = true;
            btn_delete.Visible = true;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            SaveUpdateData(2);
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            SaveUpdateData(3);
        }
    }
}
