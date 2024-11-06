using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace try_messaging
{
    public partial class startingwindow : Form
    {
        public startingwindow()
        {
            InitializeComponent();
            this.CenterToScreen();
            
        } 
        private void startingwindow_Load(object sender, EventArgs e)
        {
            

        }

        private void userBtn_Click(object sender, EventArgs e)
        {
            TenantLoginForm form4 = new TenantLoginForm();
            form4.Show(); this.Hide();
        }

        private void adminBtn_Click(object sender, EventArgs e)
        {
            
            adminLoginForm adminLoginForm = new adminLoginForm();
            adminLoginForm.Show();
            this.Hide();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
