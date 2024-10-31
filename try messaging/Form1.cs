using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace try_messaging
{
    public partial class Form1 : Form
    {
        public Form1()
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
            Form2 form4 = new Form2();
            form4.Show(); this.Hide();  

        }
    }
}
