using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class ChangePassword_admin : Form
    {
        private string adminEmail;
        private DatabaseConnection dbConnection;

        public ChangePassword_admin(string email)
        {
            InitializeComponent();
            this.adminEmail = email; // Store the admin's email
            dbConnection = new DatabaseConnection(); // Initialize the database connection
            this.CenterToParent(); // Center the form
        }

        private void ChangePassword_admin_Load(object sender, EventArgs e)
        {
            // Optional: Any initialization code if needed
        }

        private void submit_Btn_Click(object sender, EventArgs e)
        {
            string newPassword = newpasswordText.Text.Trim();
            string confirmPassword = confirmPassText.Text.Trim();

            // Validate the input
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("The new password and confirmation do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Proceed with updating the password
            if (UpdateAdminPassword(newPassword))
            {
                MessageBox.Show("Your password has been successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Close the form after success
            }
            else
            {
                MessageBox.Show("Error updating the password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool UpdateAdminPassword(string newPassword)
        {
            string query = "UPDATE admin_accounts SET password = @Password WHERE username = (SELECT username FROM admin_details WHERE email = @Email)";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Password", newPassword); // You should hash the password here
                        cmd.Parameters.AddWithValue("@Email", adminEmail);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void ChangePassword_admin_Load_1(object sender, EventArgs e)
        {

        }
    }
}
