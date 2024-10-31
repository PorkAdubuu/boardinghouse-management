using System;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class Form3 : Form
    {
        private string verificationCode; // Define verificationCode at the class level
        private int tenantId; // Store the tenant's ID

        // Constructor to accept tenant ID instead of tenant username
        public Form3(int tenantId)
        {
            InitializeComponent();
            this.tenantId = tenantId; // Store the tenant's ID
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

            // Open the form for changing password and pass the verification code and tenant ID
            Form4 form4 = new Form4(verificationCode, tenantId); // Pass the verification code and tenant ID
            form4.Show();
            this.Hide(); // Optionally hide the current form
        }

        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Generates a random 6-digit code
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
                        mailMessage.From = new MailAddress("boardinghouse24@gmail.com"); // Your email
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = false; // HTML or plain text
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            tenantcomform tenantcomform = new tenantcomform(tenantId);
            tenantcomform.Show();
            this.Hide();
        }
    }
}
