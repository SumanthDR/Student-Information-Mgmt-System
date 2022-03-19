using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_TCNew : Form
    {
        String stu_roa_id;
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStr"].ConnectionString;
        int check = 0;

        public void checkForEmptyValues()
        {
            check = 0;
            if((cmbbx_classleft.Text == "")||(cmbbx_sectionleft.Text == "")
                ||(txtbx_tcno.Text == "")||(txtbx_remarks.Text == ""))
            {
                MessageBox.Show("All Fields are compulsory");
                label_status.Text = "ERROR!, All Fields are compulsory";
                check = 1;
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
        public Frm_TCNew()
        {
            InitializeComponent();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Are you sure.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void txtbx_tcno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Prc_ViewTCRegistration", con))
                    {
                        cmd.Parameters.AddWithValue("@flag", 1);
                        cmd.Parameters.AddWithValue("@search", txtbx_admno.Text.Trim());
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                stu_roa_id = dr["roa_StuId"].ToString();
                                txtbx_sats.Text = dr["roa_StuSatsNo"].ToString();
                                txtbx_fname.Text = dr["roa_StuFirstName"].ToString();
                                txtbx_mname.Text = dr["roa_StuSecondName"].ToString();
                                txtbx_lname.Text = dr["roa_StuThirdName"].ToString();
                                txtbx_dadfname.Text = dr["roa_FatherFirstName"].ToString();
                                txtbx_dadmname.Text = dr["roa_FatherSecondName"].ToString();
                                txtbx_dadlname.Text = dr["roa_FatherThirdName"].ToString();
                                txtbx_foccu.Text = dr["roa_FatherOccupation"].ToString();
                                txtbx_momfname.Text = dr["roa_MotherFirstName"].ToString();
                                txtbx_mommname.Text = dr["roa_MotherSecondName"].ToString();
                                txtbx_momlname.Text = dr["roa_MotherThirdName"].ToString();
                                txtbx_moccu.Text = dr["roa_MotherOccupation"].ToString();
                                txtbx_dependants.Text = dr["roa_NoOfDependants"].ToString();
                                txtbx_address.Text = dr["roa_PermanentAddress"].ToString();
                                txtbx_lschool.Text = dr["roa_LastSchoolAttended"].ToString();
                                txtbx_ptcno.Text = dr["roa_PreviousTCNo"].ToString();
                                date_ptc.Text = dr["roa_PreviousTCDate"].ToString();
                                date_dob.Text = dr["roa_DOB"].ToString();
                                date_doa.Text = dr["roa_StuDOA"].ToString();
                                cmbbx_std_admitted.Text = dr["roa_StdAdmitted"].ToString();
                                cmbbx_std_section.Text = dr["roa_StdSection"].ToString();
                                cmbbx_lstd.Text = dr["roa_LastStdAdmitted"].ToString();
                                cmbbx_nationality.Text = dr["roa_Nationality"].ToString();
                                cmbbx_mtongue.Text = dr["roa_MotherTounge"].ToString();
                                cmbbx_gender.Text = dr["roa_Gender"].ToString();
                                cmbbx_religion.Text = dr["roa_Religion"].ToString();
                                txtbx_caste.Text = dr["roa_Caste"].ToString();
                                txtbx_income.Text = dr["roa_AnnualIncome"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No Record Found !");
                                ClearTextBoxes();
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
            flag = 0;
            ValidateDates();
            if (flag == 0)
            {
                SqlConnection con = new SqlConnection(connectionString);
                try
                {
                    SqlCommand cmd = new SqlCommand("Prc_TransferCertificate", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@flags", flags);
                    cmd.Parameters.AddWithValue("@tc_StuId", Frm_ViewTC.stuId);
                    cmd.Parameters.AddWithValue("@tc_roaid", Convert.ToInt32(stu_roa_id));
                    cmd.Parameters.AddWithValue("@tc_stdOnLeaving", cmbbx_classleft.Text);
                    cmd.Parameters.AddWithValue("@tc_sectionOnLeaving", cmbbx_sectionleft.Text);
                    cmd.Parameters.AddWithValue("@tc_DateOnLeaving", date_doleave.Value.Date);
                    cmd.Parameters.AddWithValue("@tc_Number", txtbx_tcno.Text.Trim());
                    cmd.Parameters.AddWithValue("@tc_Date", date_tc.Value.Date);
                    cmd.Parameters.AddWithValue("@tc_Remarks", txtbx_remarks.Text.Trim());
                    cmd.Parameters.AddWithValue("@tc_isActive", 1);
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
                finally { con.Close(); ClearTextBoxes(); }
            }
        }


        public int flag = 0;
        private void btn_save_Click(object sender, EventArgs e)
        {
            ValidateDates();
            checkForEmptyValues();
            if (check != 1)
                saveUpdateData(1);            
        }

        private void cmbbx_classleft_KeyPress(object sender, KeyPressEventArgs e)
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

        // This is an Event printPage
        // Below method is created when you "select -> printDocument1 -> Events -> PrintPage(Double Click)"
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string heading = "Transfer Certificate";

            e.Graphics.DrawString(heading, new Font("Arial", 25, FontStyle.Bold), Brushes.Black, 270, 30);
            e.Graphics.DrawString(lbl_sats.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 160);
            e.Graphics.DrawString(lbl_stuname.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 190);
            e.Graphics.DrawString(lbl_gender.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 220);
            e.Graphics.DrawString(lbl_dob.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 250);
            e.Graphics.DrawString(lbl_dadname.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 280);
            e.Graphics.DrawString(lbl_foccu.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 310);
            e.Graphics.DrawString(lbl_momname.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 340);
            e.Graphics.DrawString(lbl_moccu.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 370);
            e.Graphics.DrawString(lbl_income.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 400);
            e.Graphics.DrawString(lbl_dependants.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 430);
            e.Graphics.DrawString(lbl_nationality.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 460);
            e.Graphics.DrawString(lbl_religion.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 490);
            e.Graphics.DrawString(lbl_caste.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 520);
            e.Graphics.DrawString(lbl_mtongue.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 550);
            e.Graphics.DrawString(lbl_address.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 580);
            e.Graphics.DrawString(lbl_lschool.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 640);
            e.Graphics.DrawString(lbl_lstandard.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 670);
            e.Graphics.DrawString(lbl_ptcno.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 700);
            e.Graphics.DrawString(lbl_ptcdate.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 730);
            e.Graphics.DrawString(lbl_stdadm.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 760);
            e.Graphics.DrawString(lbl_sec.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 790);
            e.Graphics.DrawString(lbl_doa.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 820);
            e.Graphics.DrawString(lbl_classleft.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 850);
            e.Graphics.DrawString(lbl_sectionleft.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 880);
            e.Graphics.DrawString(lbl_dol.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 910);
            e.Graphics.DrawString(lbl_tcno.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 940);
            e.Graphics.DrawString(lbl_tcdate.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 970);
            e.Graphics.DrawString(lbl_remarks.Text, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, 50, 1000);


            //Print Values in Textbox

            e.Graphics.DrawString(txtbx_sats.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 160);
            e.Graphics.DrawString(txtbx_fname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 190);
            e.Graphics.DrawString(txtbx_mname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 190);
            e.Graphics.DrawString(txtbx_lname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 520, 190);
            e.Graphics.DrawString(cmbbx_gender.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 220);
            e.Graphics.DrawString(date_dob.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 250);
            e.Graphics.DrawString(txtbx_dadfname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 280);
            e.Graphics.DrawString(txtbx_dadmname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 520, 280);
            e.Graphics.DrawString(txtbx_dadlname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 640, 280);
            e.Graphics.DrawString(txtbx_foccu.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 310);
            e.Graphics.DrawString(txtbx_momfname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 340);
            e.Graphics.DrawString(txtbx_mommname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 520, 340);
            e.Graphics.DrawString(txtbx_momlname.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 640, 340);
            e.Graphics.DrawString(txtbx_moccu.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 370);
            e.Graphics.DrawString(txtbx_income.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 400);
            e.Graphics.DrawString(txtbx_dependants.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 430);
            e.Graphics.DrawString(cmbbx_nationality.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 460);
            e.Graphics.DrawString(cmbbx_religion.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 490);
            e.Graphics.DrawString(txtbx_caste.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 520);
            e.Graphics.DrawString(cmbbx_mtongue.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 550);
            e.Graphics.DrawString(txtbx_address.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 580);
            e.Graphics.DrawString(txtbx_lschool.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 640);
            e.Graphics.DrawString(cmbbx_lstd.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 670);
            e.Graphics.DrawString(txtbx_ptcno.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 700);
            e.Graphics.DrawString(date_ptc.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 730);
            e.Graphics.DrawString(cmbbx_std_admitted.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 760);
            e.Graphics.DrawString(cmbbx_std_section.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 790);
            e.Graphics.DrawString(date_doa.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 820);
            e.Graphics.DrawString(cmbbx_classleft.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 850);
            e.Graphics.DrawString(cmbbx_sectionleft.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 880);
            e.Graphics.DrawString(date_doleave.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 910);
            e.Graphics.DrawString(txtbx_tcno.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 940);
            e.Graphics.DrawString(date_tc.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 970);
            e.Graphics.DrawString(txtbx_remarks.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, 400, 1000);

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        public void ValidateDates()
        {
            int thisYear = date_tc.Value.Year;
            int ptcYear = date_ptc.Value.Year;
            int admYear = date_doa.Value.Year;
            int leaveYear = date_doleave.Value.Year;
            if (thisYear < ptcYear)
            {
                flag = 1;
                label_status.Text = "Check the Previous TC date and Current TC date";
            }

            if (admYear > leaveYear)
            {
                flag = 1;
                label_status.Text = "Check the Date of Leaving and Date of Admission";
            }
            if (cmbbx_classleft.Text != "")
            if (Convert.ToInt16(cmbbx_classleft.Text) < Convert.ToInt16(cmbbx_std_admitted.Text))
            {
                flag = 1;
                label_status.Text = "Check the standard of Leaving";
            }

        }

        private void Frm_TCNew_Load(object sender, EventArgs e)
        {
            if (Frm_ViewTC.update == 1)
            {
                cmbbx_classleft.Enabled = false;
                cmbbx_sectionleft.Enabled = false;
                date_doleave.Enabled = false;
                txtbx_tcno.Enabled = false;
                date_tc.Enabled = false;
                txtbx_remarks.Enabled = false;

                btn_search.Visible = false;

                btn_delete.Visible = false;
                btn_cancel.Visible = false;
                btn_reset.Visible = false;
                btn_save.Visible = false;
                btn_update.Visible = false;

                btn_edit.Visible = true;

                Frm_ViewTC.update = 0;


                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("Prc_ViewTCRegistration", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@flag", 2);
                cmd.Parameters.AddWithValue("@search", Frm_ViewTC.stuId);
                con.Open();
                try
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        stu_roa_id = dr["roa_StuId"].ToString();
                        txtbx_sats.Text = dr["roa_StuSatsNo"].ToString();
                        txtbx_fname.Text = dr["roa_StuFirstName"].ToString();
                        txtbx_mname.Text = dr["roa_StuSecondName"].ToString();
                        txtbx_lname.Text = dr["roa_StuThirdName"].ToString();
                        txtbx_dadfname.Text = dr["roa_FatherFirstName"].ToString();
                        txtbx_dadmname.Text = dr["roa_FatherSecondName"].ToString();
                        txtbx_dadlname.Text = dr["roa_FatherThirdName"].ToString();
                        txtbx_foccu.Text = dr["roa_FatherOccupation"].ToString();
                        txtbx_momfname.Text = dr["roa_MotherFirstName"].ToString();
                        txtbx_mommname.Text = dr["roa_MotherSecondName"].ToString();
                        txtbx_momlname.Text = dr["roa_MotherThirdName"].ToString();
                        txtbx_moccu.Text = dr["roa_MotherOccupation"].ToString();
                        txtbx_dependants.Text = dr["roa_NoOfDependants"].ToString();
                        txtbx_address.Text = dr["roa_PermanentAddress"].ToString();
                        txtbx_lschool.Text = dr["roa_LastSchoolAttended"].ToString();
                        txtbx_ptcno.Text = dr["roa_PreviousTCNo"].ToString();
                        date_ptc.Text = dr["roa_PreviousTCDate"].ToString();
                        date_dob.Text = dr["roa_DOB"].ToString();
                        date_doa.Text = dr["roa_StuDOA"].ToString();
                        cmbbx_std_admitted.Text = dr["roa_StdAdmitted"].ToString();
                        cmbbx_std_section.Text = dr["roa_StdSection"].ToString();
                        cmbbx_lstd.Text = dr["roa_LastStdAdmitted"].ToString();
                        cmbbx_nationality.Text = dr["roa_Nationality"].ToString();
                        cmbbx_mtongue.Text = dr["roa_MotherTounge"].ToString();
                        cmbbx_gender.Text = dr["roa_Gender"].ToString();
                        cmbbx_religion.Text = dr["roa_Religion"].ToString();
                        txtbx_caste.Text = dr["roa_Caste"].ToString();
                        txtbx_income.Text = dr["roa_AnnualIncome"].ToString();

                        cmbbx_classleft.Text = dr["tc_stdOnLeaving"].ToString();
                        cmbbx_sectionleft.Text = dr["tc_sectionOnLeaving"].ToString();
                        date_doleave.Text = dr["tc_DOL"].ToString();
                        txtbx_tcno.Text = dr["tc_TCNo"].ToString();
                        date_tc.Text = dr["tc_TCDate"].ToString();
                        txtbx_remarks.Text = dr["tc_Remarks"].ToString();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Something went wrong " + ex);
                }
                finally { con.Close(); }
            }
            else
            {
                cmbbx_classleft.Enabled = true;
                cmbbx_sectionleft.Enabled = true;
                date_doleave.Enabled = true;
                txtbx_tcno.Enabled = true;
                date_tc.Enabled = true;
                txtbx_remarks.Enabled = true;

                btn_search.Visible = true;

                btn_update.Visible = false;
                btn_delete.Visible = false;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            btn_edit.Visible = false;

            cmbbx_classleft.Enabled = true;
            cmbbx_sectionleft.Enabled = true;
            date_doleave.Enabled = true;
            txtbx_tcno.Enabled = true;
            date_tc.Enabled = true;
            txtbx_remarks.Enabled = true;

            btn_update.Visible = true;
            btn_cancel.Visible = true;
            btn_delete.Visible = true;

            btn_search.Visible = false;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            ValidateDates();
            checkForEmptyValues();
            if (check != 1)
                saveUpdateData(2);
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            saveUpdateData(3);
        }

        private void txtbx_remarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                base.OnKeyPress(e);
            }
        }
    }
}
