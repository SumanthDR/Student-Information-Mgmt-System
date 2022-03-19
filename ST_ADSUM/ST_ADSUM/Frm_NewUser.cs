using System;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_NewUser : Form
    {
        public Frm_NewUser()
        {
            InitializeComponent();
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

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txtbx_username.Text == "")
            {
                err_username.SetError(txtbx_username, "Please enter the username");
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Are you sure.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
        }

        private void txtbx_username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
