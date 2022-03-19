using System;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_Splash : Form
    {
        public Frm_Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 3;
            if (panel2.Width >= 633)
            {
                timer1.Stop();
                Frm_Login f = new Frm_Login();
                f.Show();
                this.Hide();
            }
        }
    }
}
