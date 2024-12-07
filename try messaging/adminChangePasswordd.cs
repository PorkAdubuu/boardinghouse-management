using MySql.Data.MySqlClient;
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
    public partial class adminChangePasswordd : Form
    {
        private int currentAdminId; // To store the admin's ID
        private int adminId;
        public adminChangePasswordd(int adminId)
        {
            InitializeComponent();
            this.currentAdminId = adminId;
            this.adminId = adminId;
        }

        private void adminChangePasswordd_Load(object sender, EventArgs e)
        {

        }
        private void UpdatePassword(string newPassword)
        {
            // SQL query to update the password in the admins_accounts table
            string query = "UPDATE admin_accounts SET password = @newPassword WHERE admin_id = @adminId;"; // Update query using adminid

            DatabaseConnection db = new DatabaseConnection(); // Create an instance of DatabaseConnection
            using (MySqlConnection connection = new MySqlConnection(db.GetConnectionString())) // Connect to the database
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newPassword", newPassword);
                        command.Parameters.AddWithValue("@adminId", currentAdminId); // Use the stored admin's ID

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

                    // Close the admin_dashboard form
                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm is admin_dashboard)
                        {
                            openForm.Close();
                            break;
                        }
                    }




                    // Close the changepassword form itself

                    startingwindow startingwindow = new startingwindow();
                    startingwindow.Show();
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
            // Query the admins_accounts table to get the current password based on addminId
            string query = "SELECT password FROM admin_accounts WHERE admin_id = @adminId";

            // Create an instance of DatabaseConnection to get the connection string
            DatabaseConnection db = new DatabaseConnection();

            using (var connection = new MySqlConnection(db.GetConnectionString()))
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@adminId", adminId);

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
