using System;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_Home : Form
    {
        public Frm_Home()
        {
            InitializeComponent();
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_RegisterofAdmission fra = new Frm_RegisterofAdmission();
            fra.MdiParent = this;
            fra.Show();
        }

        private void subjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_Subject fs = new Frm_Subject();
            fs.MdiParent = this;
            fs.Show();
        }

        private void createUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_NewUser fnu = new Frm_NewUser();
            fnu.MdiParent = this;
            fnu.Show();
        }

        private void viewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_ViewUser fvu = new Frm_ViewUser();
            fvu.MdiParent = this;
            fvu.Show();
        }

        private void transferCertificateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_TCNew ftn = new Frm_TCNew();
            ftn.MdiParent = this;
            ftn.Show();
        }

        private void sSLCRegisterationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_SSLCRegistration fsr = new Frm_SSLCRegistration();
            fsr.MdiParent = this;
            fsr.Show();
        }

        private void subjectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_ViewSubject fvs = new Frm_ViewSubject();
            fvs.MdiParent = this;
            fvs.Show();
        }

        private void registerOfAdmissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_ViewRegisterofAdmission fvros = new Frm_ViewRegisterofAdmission();
            fvros.MdiParent = this;
            fvros.Show();
        }

        private void sSLCRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_ViewSSLCRegistration fvros = new Frm_ViewSSLCRegistration();
            fvros.MdiParent = this;
            fvros.Show();
        }

        private void viewSSLCRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_ViewSSLCRegistration fvros = new Frm_ViewSSLCRegistration();
            fvros.MdiParent = this;
            fvros.Show();
        }

        private void transferCertificateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_ViewTC fvros = new Frm_ViewTC();
            fvros.MdiParent = this;
            fvros.Show();
        }

        private void Frm_Home_Load(object sender, EventArgs e)
        {
            Frm_ViewSubject.subId = "0";
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)	//check if any other child form is open, if open then close //
                ActiveMdiChild.Close();
            Frm_Authors fa = new Frm_Authors();
            fa.MdiParent = this;
            fa.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://raw.githubusercontent.com/SumanthDR/Student-Information-Mgmt-System/main/User_Manual.pdf");
        }
    }
}
