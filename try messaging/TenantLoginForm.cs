using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class TenantLoginForm : Form
    {
        public TenantLoginForm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string username = usernameInput.Text; // Get the username from the TextBox
            string password = passwordInput.Text; // Get the password from the TextBox

            DatabaseConnection db = new DatabaseConnection();

            // Validate the login credentials and retrieve the tenant ID
            int tenantId = db.GetTenantId(username, password);
            if (tenantId > 0) // Check if tenant ID is valid
            {
                MessageBox.Show("Login successful!"); // Inform the user about successful login
                Form3 form3 = new Form3(tenantId); // Pass tenant ID to Form3
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again."); // Inform the user about the failure
            }
        }

    }
}
