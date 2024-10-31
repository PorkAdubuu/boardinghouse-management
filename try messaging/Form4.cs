using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class Form4 : Form
    {
        private string verificationCode; // To store the verification code
        private int currentTenantId; // To store the tenant's ID

        public Form4(string verificationCode, int tenantId) // Change parameter to accept int tenantId
        {
            InitializeComponent();
            this.verificationCode = verificationCode; // Store the received verification code
            this.currentTenantId = tenantId; // Store the tenant's ID for password update
        }

        private void changepasswordBtn_Click(object sender, EventArgs e)
        {
            // Get the input from the text boxes
            string enteredCode = verificationText.Text;
            string newPassword = newpasswordText.Text;

            // Validate the verification code
            if (enteredCode == verificationCode)
            {
                // Update the password in the database
                UpdatePassword(newPassword);
            }
            else
            {
                MessageBox.Show("Invalid verification code. Please try again.");
            }
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
                            MessageBox.Show("Password changed successfully!");
                            this.Close(); // Close the form after successful change
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
    }
}
