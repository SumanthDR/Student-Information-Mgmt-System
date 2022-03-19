using System;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_Login : Form
    {
        string user = "admin";
        string password = "admin";
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (user == txtbx_user.Text && password == txtbx_password.Text)
            {
                Frm_Home frmhm = new Frm_Home();
                frmhm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect User or Password!");
            }
        }
    }
}
