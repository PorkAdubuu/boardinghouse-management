using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class adminverificationCode : Form
    {
        private string verificationCode; // To store the verification code
        private admin_account_profile parentForm;
        private int adminId;
        private DatabaseConnection dbConnection;

        public adminverificationCode(string verificationCode, int adminId, admin_account_profile parentForm)
        {
            InitializeComponent();
            this.verificationCode = verificationCode; // Store the received verification code
            this.adminId = adminId;
            this.parentForm = parentForm;
            dbConnection = new DatabaseConnection();
        }

        private void adminverificationCode_Load(object sender, EventArgs e)
        {

        }

        private void vcodeBtn_Click(object sender, EventArgs e)
        {
            DatabaseConnection db = new DatabaseConnection();
            string adminEmail = db.GetAdminEmail(adminId); // Get the actual email from the database

            if (string.IsNullOrEmpty(adminEmail))
            {
                MessageBox.Show("Email not found for the given tenant ID.");
                return; // Exit if email is not found
            }


            // Generate the verification code
            verificationCode = GenerateVerificationCode();

            // Send the verification code via email
            SendEmail(adminEmail, "Verification Code", $"Your verification code is: {verificationCode}");
        }
        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Generates a random 6-digit code
        }
        public string GetAdminEmail(int adminId)
        {
            string email = string.Empty;
            string query = "SELECT email FROM admin_details WHERE admin_details_id = @adminId;";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                conn.Open();

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@adminId", adminId);
                    try
                    {
                        email = command.ExecuteScalar() as string; // Retrieve the email
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error fetching email: " + ex.Message);
                    }
                }
            }

            return email; // Return the fetched email
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

                MessageBox.Show("6-digit verification code has been sent your email");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message); // Detailed error message
            }
        }

        private void SubmitCodeBtn_Click(object sender, EventArgs e)
        {
            // Get the input from the text box
            string enteredCode = verificationText.Text;

            // Validate the verification code
            if (enteredCode == verificationCode)
            {
                // Initialize the ChangePassword form
                adminChangePasswordd adminChangePasswordd = new adminChangePasswordd(adminId);

                // Set up the form to be displayed inside the changePassPanel of tenant_profile
                adminChangePasswordd.TopLevel = false; // Make sure it's not a top-level form
                adminChangePasswordd.FormBorderStyle = FormBorderStyle.None; // Remove border
                adminChangePasswordd.Dock = DockStyle.Fill; // Ensure it fills the panel

                if (parentForm != null && parentForm.panel1 != null)
                {
                    // Clear previous controls in the panel if any
                    if (parentForm.panel1.Controls.Count > 0)
                    {
                        parentForm.panel1.Controls.Clear();
                    }

                    // Add the ChangePassword form to the panel and display it
                    parentForm.panel1.Controls.Add(adminChangePasswordd);
                    adminChangePasswordd.Show();
                }
                else
                {
                    MessageBox.Show("Parent form or panel1 is not initialized properly.");
                }


            }
            else
            {
                MessageBox.Show("Invalid verification code. Please try again.");
            }
        }
    }
}
