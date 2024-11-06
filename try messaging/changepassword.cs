using System;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class changepassword : Form
    {
        
        private int currentTenantId; // To store the tenant's ID
        private int tenantId;

        public changepassword(int tenantId) // Change parameter to accept int tenantId
        {
            InitializeComponent();
            this.tenantId = tenantId;
            
            this.currentTenantId = tenantId; // Store the tenant's ID for password update
        }

        

        private void UpdatePassword(string newPassword)
        {
            // SQL query to update the password in the tenants_accounts table
            string query = "UPDATE tenants_accounts SET password = @newPassword WHERE tenid = @tenid;"; // Update query using tenid

            DatabaseConnection db = new DatabaseConnection(); // Create an instance of DatabaseConnection
            using (MySqlConnection connection = new MySqlConnection(db.GetConnectionString())) // Connect to the database
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newPassword", newPassword);
                        command.Parameters.AddWithValue("@tenid", currentTenantId); // Use the stored tenant's ID

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {                           
                        }
                        else
                        {
                            MessageBox.Show("Error: Password change failed. Please try again.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating password: " + ex.Message);
                }
            }
        }

        private void changepassword_Load(object sender, EventArgs e)
        {

        }

        private void submitPassBtn_Click(object sender, EventArgs e)
        {
            // Get the input from the text boxes
            string currentPassword = currentPassText.Text;
            string newPassword = newpasswordText.Text;
            string confirmPassword = confirmPassText.Text;

            // Check if the current password matches the password in the database
            if (CheckCurrentPassword(currentPassword))
            {
                // Check if the new password and confirm password match
                if (newPassword == confirmPassword)
                {
                    // Update the password in the database
                    UpdatePassword(newPassword);

                    // Display a success message
                    MessageBox.Show("Password updated successfully, please log in again.");

                    // Close the tenant_dashboard form
                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm is tenant_dashboard)
                        {
                            openForm.Close();
                            break;
                        }
                    }

                    // Show the tenantLoginForm
                    TenantLoginForm loginForm = new TenantLoginForm();
                    loginForm.Show();

                    // Close the changepassword form itself
                    this.Close();
                }
                else
                {
                    MessageBox.Show("New password and confirm password do not match.");
                }
            }
            else
            {
                MessageBox.Show("Current password is incorrect.");
            }
        }

        private bool CheckCurrentPassword(string enteredPassword)
        {
            // Query the tenants_accounts table to get the current password based on tenantId
            string query = "SELECT password FROM tenants_accounts WHERE tenid = @tenantId";

            // Create an instance of DatabaseConnection to get the connection string
            DatabaseConnection db = new DatabaseConnection();

            using (var connection = new MySqlConnection(db.GetConnectionString()))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tenantId", tenantId); 

                    var result = command.ExecuteScalar();
                    if (result != null && result.ToString() == enteredPassword)
                    {
                        return true; // Password matches
                    }
                    else
                    {
                        return false; // Password does not match
                    }
                }
            }
        }

        private void showPassword_Click(object sender, EventArgs e)
        {
            if (currentPassText.PasswordChar == '*' || newpasswordText.PasswordChar == '*' || confirmPassText.PasswordChar == '*')
            {
                currentPassText.PasswordChar = '\0';
                newpasswordText.PasswordChar = '\0';
                confirmPassText.PasswordChar = '\0';
            }
            else
            {
                currentPassText.PasswordChar = '*';
                newpasswordText.PasswordChar = '*';
                confirmPassText.PasswordChar = '*';
            }
        }
    }
}
