using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class adminLoginForm : Form
    {
        public adminLoginForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.KeyPreview = true; 
            this.KeyDown += new KeyEventHandler(adminLoginForm_KeyDown); // Add KeyDown event handler
        }
        private void adminLoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginBtn_Click(sender, e); // Call the login function when Enter is pressed
                e.Handled = true; // Mark the event as handled
                e.SuppressKeyPress = true; // Suppress the Enter key's default behavior
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void adminLoginForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = usernameInput.Text; // Get the username from the TextBox
            string password = passwordInput.Text; // Get the password from the TextBox

            DatabaseConnection db = new DatabaseConnection();

            // Validate the login credentials and retrieve the admin ID
            int adminID = db.GetAdminId(username, password);
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

        private void backBtn_Click(object sender, EventArgs e)
        {
            startingwindow startingwindow = new startingwindow();
            startingwindow.Show();
            this.Hide();
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
