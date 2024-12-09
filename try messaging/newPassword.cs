using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class newPassword : Form
    {
        private string tenantEmail;
        private DatabaseConnection dbConnection;

        public newPassword(string email)
        {
            InitializeComponent();
            tenantEmail = email; // Pass the tenant's email to the form
            this.CenterToParent(); // Center the form
            dbConnection = new DatabaseConnection(); // Initialize your database connection class
        }

        private void showPassword_Click(object sender, EventArgs e)
        {
            // Toggle password visibility
            bool isPasswordHidden = newpasswordText.PasswordChar == '*';
            newpasswordText.PasswordChar = isPasswordHidden ? '\0' : '*';
            confirmPassText.PasswordChar = isPasswordHidden ? '\0' : '*';
        }

        private void UpdatePassword(string email, string newPassword)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString())) // Use your DatabaseConnection class
                {
                    conn.Open();

                    string query = @"
                        UPDATE tenants_accounts 
                        SET password = @password 
                        WHERE tenid = (
                            SELECT tenid 
                            FROM tenants_details 
                            WHERE email = @tenantEmail
                        )";

                    using (var command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@password", newPassword); // Use plain text password here
                        command.Parameters.AddWithValue("@tenantEmail", email);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password has been successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); // Close the form after success
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void submit_Btn_Click(object sender, EventArgs e)
        {
            string newPassword = newpasswordText.Text.Trim();
            string confirmPassword = confirmPassText.Text.Trim();

            // Debug to ensure passwords are correctly retrieved
            if (newPassword.Length == 0 || confirmPassword.Length == 0)
            {
                MessageBox.Show("Passwords cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Debug log for testing (remove or replace with logging in production)
            Console.WriteLine($"Entered Password: {newPassword}");
            Console.WriteLine($"Confirmed Password: {confirmPassword}");

            // Update the password in the database without hashing
            UpdatePassword(tenantEmail, newPassword); // Use plain text password
        }

        private void newPassword_Load(object sender, EventArgs e)
        {
            // Additional initialization logic (if needed)
        }
    }
}
