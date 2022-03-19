using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_SSLCRegistration : Form
    {
        int check = 0;
        int comboBoxCheck = 0;        
        public Frm_SSLCRegistration()
        {
            InitializeComponent();
        }

        public int flag = 0, flag1 = 0;
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
            {
                err.SetError(txtbx, "");                
            }                
        }

        //This function is written to inform user to fill the combo boxes
        public void ValidateValuesComboBox(ComboBox cmb, ErrorProvider err)
        {            
            if (cmb.Text == "")
            {
                err.SetError(cmb, "This Field is complusory");
                label_status.Text = "ERROR !, Please check the Red Icon";
                flag1 = 1;
            }
            else
            {
                err.SetError(cmb, "");                
            }                
        }

        public void ValidateValues()
        {
            flag = flag1 = 0;

            ValidateValuesText(txtbx_adhaar, errorProvider_adhaar);
            ValidateValuesText(txtbx_dadmobile, errorProvider_dadmobile);
            ValidateValuesText(txtbx_incomeno, errorProvider_incomeno);
            ValidateValuesText(txtbx_pcondition, errorProvider_pcondition);
            ValidateValuesText(txtbx_fee, errorProvider_fee);
            ValidateValuesText(txtbx_Bank_name, errorProvider_BankName);
            ValidateValuesText(txtbx_ifsc, errorProvider_IFSC);
            ValidateValuesText(txtbx_accountno, errorProvider_AccountNo);

            ValidateValuesComboBox(cmbbx_scategory, errorProvider_category);
        }

        String stu_roa_id;
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;

        public void validateForSameSubjects()
        {
            comboBoxCheck = 0;
            if ((cmbbx_csub1.Text == cmbbx_csub2.Text) || (cmbbx_csub1.Text == cmbbx_csub3.Text))
                comboBoxCheck = 1;
            else if (cmbbx_csub2.Text == cmbbx_csub3.Text)
                comboBoxCheck = 1;
            else if ((cmbbx_flanguage.Text == cmbbx_slanguage.Text) || (cmbbx_flanguage.Text == cmbbx_tlanguage.Text))
                comboBoxCheck = 1;
            else if (cmbbx_slanguage.Text == cmbbx_tlanguage.Text)
                comboBoxCheck = 1;
            if (comboBoxCheck == 1)
            {
                MessageBox.Show("Choose Different Subjects & Languages");
                comboBoxCheck = 0;
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
        
        public void displaySubjectsComboBox(ComboBox cmb)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("prc_DisplaySubjects", con);
                if (cmb == cmbbx_csub1 || cmb == cmbbx_csub2 || cmb == cmbbx_csub3)
                    cmd.Parameters.AddWithValue("@subject_type", "CORE");
                else
                    cmd.Parameters.AddWithValue("@subject_type", "LANGUAGE");
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                cmb.DataSource = dt;
                cmb.DisplayMember = "sub_Name";
                cmb.ValueMember = "sub_Id";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong " + ex);
            }
            finally { con.Close(); }
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();

            txtbx_dise.Text = "29160925206";
            txtbx_kseeb.Text = "DD0070";
        }

        //function to accept only numbers
        private void txtbx_adhaar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Event function to accept float values as well
        private void txtbx_fee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Are you sure.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void Frm_SSLCRegistration_Load(object sender, EventArgs e)
        {
            if (Frm_ViewSSLCRegistration.update == 1)
            {
                txtbx_adhaar.Enabled = false;

                txtbx_dadmobile.Enabled = false;

                cmbbx_scategory.Enabled = false;

                txtbx_incomeno.Enabled = false;

                txtbx_pcondition.Enabled = false;

                cmbbx_medium_of_instruction.Enabled = false;
                txtbx_fee.Enabled = false;

                cmbbx_flanguage.Enabled = false;
                cmbbx_slanguage.Enabled = false;
                cmbbx_tlanguage.Enabled = false;

                cmbbx_csub1.Enabled = false;
                cmbbx_csub2.Enabled = false;
                cmbbx_csub3.Enabled = false;

                txtbx_Bank_name.Enabled = false;
                txtbx_ifsc.Enabled = false;

                txtbx_accountno.Enabled = false;

                btn_cancel.Visible = false;
                btn_reset.Visible = false;
                btn_save.Visible = false;

                btn_edit.Visible = true;

                btn_delete.Visible = false;
                btn_update.Visible = false;

                btn_edit_comboCoreSub.Visible = false;
                btn_edit_comboLang.Visible = false;

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("prc_ViewSearchSSLCRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@flag", 2);
                cmd.Parameters.AddWithValue("@search", Frm_ViewSSLCRegistration.stuId);
                con.Open();
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        txtbx_admno_sats.Text = dr["roa_StuAdmissionNo"].ToString();
                        txtbx_fname.Text = dr["roa_StuFirstName"].ToString();
                        txtbx_mname.Text = dr["roa_StuSecondName"].ToString();
                        txtbx_lname.Text = dr["roa_StuThirdName"].ToString();
                        txtbx_sats.Text = dr["roa_StuSatsNo"].ToString();
                        txtbx_dadfname.Text = dr["roa_FatherFirstName"].ToString();
                        txtbx_dadmname.Text = dr["roa_FatherSecondName"].ToString();
                        txtbx_dadlname.Text = dr["roa_FatherThirdName"].ToString();
                        txtbx_momfname.Text = dr["roa_MotherFirstName"].ToString();
                        txtbx_mommname.Text = dr["roa_MotherSecondName"].ToString();
                        txtbx_momlname.Text = dr["roa_MotherThirdName"].ToString();
                        txtbx_address.Text = dr["roa_PermanentAddress"].ToString();
                        date_dob.Text = dr["roa_DOB"].ToString();
                        txtbx_gender.Text = dr["roa_Gender"].ToString();
                        txtbx_religion.Text = dr["roa_Religion"].ToString();
                        txtbx_caste.Text = dr["roa_Caste"].ToString();
                        txtbx_income.Text = dr["roa_AnnualIncome"].ToString();

                        txtbx_adhaar.Text = dr["sr_StuAdhaarNo"].ToString();

                        txtbx_dadmobile.Text = dr["sr_StuFatherPhoneNo"].ToString();

                        cmbbx_scategory.Text = dr["sr_StuSocialCategory"].ToString();

                        txtbx_incomeno.Text = dr["sr_IncomeNo"].ToString();

                        txtbx_pcondition.Text = dr["sr_StuPhysicalCondition"].ToString();

                        cmbbx_medium_of_instruction.Text = dr["sr_MediumOfInstruction"].ToString();
                        txtbx_fee.Text = dr["sr_Fee"].ToString();

                        
                        displaySubjectsComboBoxForUpdate(cmbbx_csub1);
                        displaySubjectsComboBoxForUpdate(cmbbx_csub2);
                        displaySubjectsComboBoxForUpdate(cmbbx_csub3);
                        displaySubjectsComboBoxForUpdate(cmbbx_flanguage);
                        displaySubjectsComboBoxForUpdate(cmbbx_slanguage);
                        displaySubjectsComboBoxForUpdate(cmbbx_tlanguage);

                        txtbx_Bank_name.Text = dr["sr_BankName"].ToString();
                        txtbx_ifsc.Text = dr["sr_IFSCcode"].ToString();

                        txtbx_accountno.Text = dr["sr_BankAccNo"].ToString();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Something went wrong " + ex);
                }
                finally { con.Close(); }

                Frm_ViewSSLCRegistration.update = 0;
            }
            else
            {
                displaySubjectsComboBox(cmbbx_csub1);
                displaySubjectsComboBox(cmbbx_csub2);
                displaySubjectsComboBox(cmbbx_csub3);

                displaySubjectsComboBox(cmbbx_flanguage);
                displaySubjectsComboBox(cmbbx_slanguage);
                displaySubjectsComboBox(cmbbx_tlanguage);

                txtbx_adhaar.Enabled = true;

                txtbx_dadmobile.Enabled = true;

                cmbbx_scategory.Enabled = true;

                txtbx_incomeno.Enabled = true;

                txtbx_pcondition.Enabled = true;

                cmbbx_medium_of_instruction.Enabled = true;
                txtbx_fee.Enabled = true;

                cmbbx_flanguage.Enabled = true;
                cmbbx_slanguage.Enabled = true;
                cmbbx_tlanguage.Enabled = true;

                cmbbx_csub1.Enabled = true;
                cmbbx_csub2.Enabled = true;
                cmbbx_csub3.Enabled = true;

                txtbx_Bank_name.Enabled = true;
                txtbx_ifsc.Enabled = true;

                txtbx_accountno.Enabled = true;

                btn_delete.Visible = false;
                btn_update.Visible = false;
            }
        }

        public void displaySubjectsComboBoxForUpdate(ComboBox cmb)
        {
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("Prc_ViewSubjectSSLCForUpdate", con);
                cmd.Parameters.AddWithValue("@studentid", txtbx_admno_sats.Text);
                if (cmb == cmbbx_flanguage)
                    cmd.Parameters.AddWithValue("@flag", 4);
                else if (cmb == cmbbx_slanguage)
                    cmd.Parameters.AddWithValue("@flag", 5);
                else if (cmb == cmbbx_tlanguage)
                    cmd.Parameters.AddWithValue("@flag", 6);
                else if (cmb == cmbbx_csub1)
                    cmd.Parameters.AddWithValue("@flag", 1);
                else if (cmb == cmbbx_csub2)
                    cmd.Parameters.AddWithValue("@flag", 2);
                else if (cmb == cmbbx_csub3)
                    cmd.Parameters.AddWithValue("@flag", 3);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                cmb.DisplayMember = "sub_Name";
                cmb.DataSource = dt;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Something went wrong !!! \n" + e);
            }
            finally { con.Close(); }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Prc_ViewRegisterofAdmission", con))
                    {
                        cmd.Parameters.AddWithValue("@flag", 1);
                        cmd.Parameters.AddWithValue("@search", txtbx_admno_sats.Text.Trim());
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                stu_roa_id = dr["roa_StuId"].ToString();
                                txtbx_fname.Text = dr["roa_StuFirstName"].ToString();
                                txtbx_mname.Text = dr["roa_StuSecondName"].ToString();
                                txtbx_lname.Text = dr["roa_StuThirdName"].ToString();
                                txtbx_sats.Text = dr["roa_StuSatsNo"].ToString();
                                txtbx_dadfname.Text = dr["roa_FatherFirstName"].ToString();
                                txtbx_dadmname.Text = dr["roa_FatherSecondName"].ToString();
                                txtbx_dadlname.Text = dr["roa_FatherThirdName"].ToString();
                                txtbx_momfname.Text = dr["roa_MotherFirstName"].ToString();
                                txtbx_mommname.Text = dr["roa_MotherSecondName"].ToString();
                                txtbx_momlname.Text = dr["roa_MotherThirdName"].ToString();
                                txtbx_address.Text = dr["roa_PermanentAddress"].ToString();
                                date_dob.Text = dr["roa_DOB"].ToString();
                                txtbx_gender.Text = dr["roa_Gender"].ToString();
                                txtbx_religion.Text = dr["roa_Religion"].ToString();
                                txtbx_caste.Text = dr["roa_Caste"].ToString();
                                txtbx_income.Text = dr["roa_AnnualIncome"].ToString();

                            }
                            else
                            {
                                MessageBox.Show("No Record Found !");
                                ClearTextBoxes();
                                txtbx_dise.Text = "29160925206";
                                txtbx_kseeb.Text = "DD0070";
                            }
                        }
                        con.Close();
                        label_status.Text = "...";
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Something went wrong " + ex);
            }
        }

        public void saveUpdateData(int flags)
        {
            if ((txtbx_adhaar.Text.Length != 12) || (txtbx_dadmobile.Text.Length != 10))
            {
                MessageBox.Show("Check Mobile number and Adhaar once again");
            }
            else if((flag == 1)||(flag1 == 1))
                MessageBox.Show("Check the fields with Red Icons");
            else
            {
                SqlConnection con = new SqlConnection(connectionString);
                try
                {
                    SqlCommand cmd = new SqlCommand("Prc_SSLCRegistrationInsert", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@flags", flags);
                    cmd.Parameters.AddWithValue("@stuId", Frm_ViewSSLCRegistration.stuId);
                    cmd.Parameters.AddWithValue("@roa_id", Convert.ToInt32(stu_roa_id));
                    cmd.Parameters.AddWithValue("@kseeb_code", txtbx_kseeb.Text);
                    cmd.Parameters.AddWithValue("@dise_Code", txtbx_dise.Text);
                    cmd.Parameters.AddWithValue("@sr_adhaarNo", txtbx_adhaar.Text.Trim());
                    cmd.Parameters.AddWithValue("@sr_fatherPhoneNo", txtbx_dadmobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@sr_SocialCategory", cmbbx_scategory.Text);
                    cmd.Parameters.AddWithValue("@sr_PhysicalCondition", txtbx_pcondition.Text.Trim());
                    cmd.Parameters.AddWithValue("@sr_MediumofInstruction", cmbbx_medium_of_instruction.Text);
                    cmd.Parameters.AddWithValue("@sr_Fee", txtbx_fee.Text.Trim());
                    cmd.Parameters.AddWithValue("@sr_Language1_id", cmbbx_flanguage.SelectedValue);
                    cmd.Parameters.AddWithValue("@sr_Language2_id", cmbbx_slanguage.SelectedValue);
                    cmd.Parameters.AddWithValue("@sr_Language3_id", cmbbx_tlanguage.SelectedValue);
                    cmd.Parameters.AddWithValue("@sr_CoreSubject1", cmbbx_csub1.SelectedValue);
                    cmd.Parameters.AddWithValue("@sr_CoreSubject2", cmbbx_csub2.SelectedValue);
                    cmd.Parameters.AddWithValue("@sr_CoreSubject3", cmbbx_csub3.SelectedValue);
                    cmd.Parameters.AddWithValue("@sr_BankName", txtbx_Bank_name.Text.Trim());
                    cmd.Parameters.AddWithValue("@sr_IFSCode", txtbx_ifsc.Text.Trim());
                    cmd.Parameters.AddWithValue("@sr_BankAccNo", txtbx_accountno.Text.Trim());
                    cmd.Parameters.AddWithValue("@sr_isActive", 1);
                    cmd.Parameters.AddWithValue("@sr_IncomeNo", txtbx_incomeno.Text.Trim());
                    cmd.Parameters.AddWithValue("@date_SSLCLog", datePicker_SSLC.Value.Date);
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
                txtbx_dise.Text = "29160925206";
                txtbx_kseeb.Text = "DD0070";
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ValidateValues();
            validateForSameSubjects();
            if (comboBoxCheck != 1)
                saveUpdateData(1);
            comboBoxCheck = 0;
        }

        private void cmbbx_medium_of_instruction_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtbx_pcondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbbx_medium_of_instruction_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbbx_flanguage_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        // Drag and drop "printDialog" and "printDocument"
        // This is a button method
        private void btn_print_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Print Labels

            string heading = "SSLC REGISTRATION FORM";

            e.Graphics.DrawString(heading, new Font("Arial", 25, FontStyle.Bold), Brushes.Black, 200, 30);
            e.Graphics.DrawString(lbl_admission.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 160);
            e.Graphics.DrawString(lbl_kseeb.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 190);
            e.Graphics.DrawString(lbl_dise.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 220);
            e.Graphics.DrawString(lbl_stuname.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 250);
            e.Graphics.DrawString(lbl_sats.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 280);
            e.Graphics.DrawString(lbl_adhaar.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 310);
            e.Graphics.DrawString(lbl_dadname.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 340);
            e.Graphics.DrawString(lbl_momname.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 370);
            e.Graphics.DrawString(lbl_address.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 400);
            e.Graphics.DrawString(lbl_mobile.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 460);
            e.Graphics.DrawString(lbl_dob.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 490);
            e.Graphics.DrawString(lbl_gender.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 520);
            e.Graphics.DrawString(lbl_religion.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 550);
            e.Graphics.DrawString(lbl_category.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 580);
            e.Graphics.DrawString(lbl_caste.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 610);
            e.Graphics.DrawString(lbl_incomeno.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 640);
            e.Graphics.DrawString(lbl_annual_income.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 670);
            e.Graphics.DrawString(lbl_pcondition.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 700);
            e.Graphics.DrawString(lbl_moinstruction.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 730);
            e.Graphics.DrawString(lbl_fee.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 760);
            e.Graphics.DrawString(lbl_flanguage.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 790);
            e.Graphics.DrawString(lbl_slanguage.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 820);
            e.Graphics.DrawString(lbl_tlanguage.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 850);
            e.Graphics.DrawString(lbl_csub1.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 880);
            e.Graphics.DrawString(lbl_csub2.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 910);
            e.Graphics.DrawString(lbl_csub3.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 940);
            e.Graphics.DrawString(lbl_bankname.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 970);
            e.Graphics.DrawString(lbl_ifsc.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 1000);
            e.Graphics.DrawString(lbl_accountno.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 1030);

            //Print Values in Textbox          
            e.Graphics.DrawString(txtbx_admno_sats.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 160);
            e.Graphics.DrawString(txtbx_kseeb.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 190);
            e.Graphics.DrawString(txtbx_dise.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 220);
            e.Graphics.DrawString(txtbx_fname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 250);
            e.Graphics.DrawString(txtbx_mname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 520, 250);
            e.Graphics.DrawString(txtbx_lname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 640, 250);
            e.Graphics.DrawString(txtbx_sats.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 280);
            e.Graphics.DrawString(txtbx_adhaar.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 310);
            e.Graphics.DrawString(txtbx_dadfname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 340);
            e.Graphics.DrawString(txtbx_dadmname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 520, 340);
            e.Graphics.DrawString(txtbx_dadlname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 640, 340);
            e.Graphics.DrawString(txtbx_momfname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 370);
            e.Graphics.DrawString(txtbx_mommname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 520, 370);
            e.Graphics.DrawString(txtbx_momlname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 640, 370);
            e.Graphics.DrawString(txtbx_address.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 400);
            e.Graphics.DrawString(txtbx_dadmobile.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 460);
            e.Graphics.DrawString(date_dob.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 490);
            e.Graphics.DrawString(txtbx_gender.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 520);
            e.Graphics.DrawString(txtbx_religion.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 550);
            e.Graphics.DrawString(cmbbx_scategory.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 580);
            e.Graphics.DrawString(txtbx_caste.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 610);
            e.Graphics.DrawString(txtbx_incomeno.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 640);
            e.Graphics.DrawString(txtbx_income.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 670);
            e.Graphics.DrawString(txtbx_pcondition.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 700);
            e.Graphics.DrawString(cmbbx_medium_of_instruction.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 730);
            e.Graphics.DrawString(txtbx_fee.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 760);
            e.Graphics.DrawString(cmbbx_flanguage.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 790);
            e.Graphics.DrawString(cmbbx_slanguage.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 820);
            e.Graphics.DrawString(cmbbx_tlanguage.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 850);
            e.Graphics.DrawString(cmbbx_csub1.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 880);
            e.Graphics.DrawString(cmbbx_csub2.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 910);
            e.Graphics.DrawString(cmbbx_csub3.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 940);
            e.Graphics.DrawString(txtbx_Bank_name.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 970);
            e.Graphics.DrawString(txtbx_ifsc.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 1000);
            e.Graphics.DrawString(txtbx_accountno.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 1030);

        }

        private void toolStripPrint_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void txtbx_Bank_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }
        }

        private void txtbx_accountno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }
        }

        private void txtbx_incomeno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            txtbx_adhaar.Enabled = true;

            txtbx_dadmobile.Enabled = true;

            cmbbx_scategory.Enabled = true;

            txtbx_incomeno.Enabled = true;

            txtbx_pcondition.Enabled = true;

            cmbbx_medium_of_instruction.Enabled = true;
            txtbx_fee.Enabled = true;



            txtbx_Bank_name.Enabled = true;
            txtbx_ifsc.Enabled = true;

            txtbx_accountno.Enabled = true;

            btn_edit.Visible = false;

            btn_delete.Visible = true;
            btn_cancel.Visible = true;
            btn_update.Visible = true;
            btn_edit_comboLang.Visible = true;
            btn_edit_comboCoreSub.Visible = true;

            // Frm_ViewSSLCRegistration.update = 0;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            ValidateValues();
            validateForSameSubjects();
            if (comboBoxCheck != 1)
                saveUpdateData(2);
            comboBoxCheck = 0;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            validateForSameSubjects();
            if (comboBoxCheck != 1)
                saveUpdateData(3);
            comboBoxCheck = 0;
        }

        private void btn_edit_comboLang_Click(object sender, EventArgs e)
        {
            displaySubjectsComboBox(cmbbx_flanguage);
            displaySubjectsComboBox(cmbbx_slanguage);
            displaySubjectsComboBox(cmbbx_tlanguage);

            cmbbx_flanguage.Enabled = true;
            cmbbx_slanguage.Enabled = true;
            cmbbx_tlanguage.Enabled = true;
        }

        private void btn_edit_comboCoreSub_Click(object sender, EventArgs e)
        {
            displaySubjectsComboBox(cmbbx_csub1);
            displaySubjectsComboBox(cmbbx_csub2);
            displaySubjectsComboBox(cmbbx_csub3);

            cmbbx_csub1.Enabled = true;
            cmbbx_csub2.Enabled = true;
            cmbbx_csub3.Enabled = true;
        }
    }
}
