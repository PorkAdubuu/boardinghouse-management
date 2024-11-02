using System;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class changepassword : Form
    {
        private string verificationCode; // To store the verification code
        private int currentTenantId; // To store the tenant's ID
        private int tenantId;

        public changepassword(string verificationCode, int tenantId) // Change parameter to accept int tenantId
        {
            InitializeComponent();
            this.tenantId = tenantId;
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

        private void changepassword_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseConnection db = new DatabaseConnection();
            string tenantEmail = db.GetTenantEmail(tenantId); // Get the actual email from the database

            if (string.IsNullOrEmpty(tenantEmail))
            {
                MessageBox.Show("Email not found for the given tenant ID.");
                return; // Exit if email is not found
            }

            // Generate the verification code
            verificationCode = GenerateVerificationCode();

            // Send the verification code via email
            SendEmail(tenantEmail, "Verification Code", $"Your verification code is: {verificationCode}");
        }

        private void SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                // Set the security protocol
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Use TLS 1.2

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)) // Use port 587
                {
                    smtpClient.Credentials = new NetworkCredential("boardinghouse24@gmail.com", "cjzvmzmwrspxxkxh"); // Your email and app password
                    smtpClient.EnableSsl = true; // Enable SSL

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("boardinghouse24@gmail.com"); // email
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = false;
                        mailMessage.To.Add(toAddress); // Add recipient email

                        // Send the email
                        smtpClient.Send(mailMessage);
                    }
                }

                MessageBox.Show("Email sent successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message); // Detailed error message
            }
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Generates a random 6-digit code
        }
    }
}
