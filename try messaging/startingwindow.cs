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
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(tenantLoginForm_KeyDown); // Add KeyDown event handler
            this.CenterToScreen();
            
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
        private void startingwindow_Load(object sender, EventArgs e)
        {
            

        }

        private void adminBtn_Click(object sender, EventArgs e)
        {

        }

        private void userBtn_Click(object sender, EventArgs e)
        {

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string username = usernameInput.Text; // Get the username from the TextBox
            string password = passwordInput.Text; // Get the password from the TextBox

            DatabaseConnection db = new DatabaseConnection();

            // Validate the login credentials and retrieve the tenant ID
            int tenantId = db.GetTenantId(username, password);
            int adminID = db.GetAdminId(username, password);
            if (tenantId > 0) // Check if tenant ID is valid
            {
                MessageBox.Show("Login successful!"); // Inform the user about successful login
                GlobalSettings.LoggedInTenantId = tenantId; // Set the logged-in tenant ID
                tenant_dashboard form3 = new tenant_dashboard(tenantId); // Pass tenant ID to Form3
                form3.Show();
                this.Hide();
            }
            else
            {
                if (adminID > 0) // Check if admin ID is valid
                {
                    // Store the logged-in admin's ID in the static property
                    GlobalSettings.LoggedInAdminId = adminID;  // Set the logged-in admin ID

                    MessageBox.Show("Login successful!"); // Inform the user about successful login

                    // Proceed to the admin dashboard with the logged-in admin ID
                    admin_dashboard admin_Dashboard = new admin_dashboard(adminID);
                    admin_Dashboard.Show();
                    this.Hide();  // Hide the login form
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again."); // Inform the user about the failure
                }

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

        
    }
}
