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
            this.CenterToScreen();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(tenantLoginForm_KeyDown); // Add KeyDown event handler
        }
        private void tenantLoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pictureBox3_Click(sender, e); // Call the login function when Enter is pressed
                e.Handled = true; // Mark the event as handled
                e.SuppressKeyPress = true; // Suppress the Enter key's default behavior
            }
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
                tenant_dashboard form3 = new tenant_dashboard(tenantId); // Pass tenant ID to Form3
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again."); // Inform the user about the failure
            }
        }

        private void TenantLoginForm_Load(object sender, EventArgs e)
        {

        }

        private void passwordInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void usernameInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string username = usernameInput.Text; // Get the username from the TextBox
            string password = passwordInput.Text; // Get the password from the TextBox

            DatabaseConnection db = new DatabaseConnection();

            // Validate the login credentials and retrieve the tenant ID
            int tenantId = db.GetTenantId(username, password);
            if (tenantId > 0) // Check if tenant ID is valid
            {
                MessageBox.Show("Login successful!"); // Inform the user about successful login
                tenant_dashboard form3 = new tenant_dashboard(tenantId); // Pass tenant ID to Form3
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again."); // Inform the user about the failure
            }
        }

        private void showPassword_Click(object sender, EventArgs e)
        {
            
            if (passwordInput.PasswordChar == '*')
            {
                passwordInput.PasswordChar = '\0'; 
            }
            else
            {
                passwordInput.PasswordChar = '*'; 
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            startingwindow startingwindow = new startingwindow();
            startingwindow.Show();
            this.Hide();
        }
    }
}
