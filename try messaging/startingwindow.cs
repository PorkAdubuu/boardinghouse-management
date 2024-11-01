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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminsideform adminsideform = new adminsideform();
            adminsideform.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TenantLoginForm form4 = new TenantLoginForm();
            form4.Show(); this.Hide();  

        }

        private void startingwindow_Load(object sender, EventArgs e)
        {

        }
    }
}
