using System;
using System.Windows.Forms;

namespace ST_ADSUM
{
    public partial class Frm_ViewUser : Form
    {
        public Frm_ViewUser()
        {
            InitializeComponent();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "User Details";
            printer.SubTitle = "";
            printer.PrintDataGridView(dataGridView1);
        }
    }
}
