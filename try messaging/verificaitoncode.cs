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
    public partial class verificaitoncode : Form
    {
        private string verificationCode; // To store the verification code
        private int currentTenantId; // To store the tenant's ID
        private int tenantId;
        private tenant_profile parentForm;
        public verificaitoncode(string verificationCode, int tenantId, tenant_profile parentForm)
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#f7f7f7");
            this.tenantId = tenantId;
            this.verificationCode = verificationCode; // Store the received verification code
            this.currentTenantId = tenantId; // Store the tenant's ID for password update
            this.parentForm = parentForm;
        }

        private void vcodeBtn_Click(object sender, EventArgs e)
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

                MessageBox.Show("6-digit verification code has been sent your email");
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

        private void SubmitCodeBtn_Click(object sender, EventArgs e)
        {
            // Get the input from the text box
            string enteredCode = verificationText.Text;

            // Validate the verification code
            if (enteredCode == verificationCode)
            {
                // Initialize the ChangePassword form
                changepassword changePasswordForm = new changepassword(tenantId);

                // Set up the form to be displayed inside the changePassPanel of tenant_profile
                changePasswordForm.TopLevel = false; // Make sure it's not a top-level form
                changePasswordForm.FormBorderStyle = FormBorderStyle.None; // Remove border
                changePasswordForm.Dock = DockStyle.Fill; // Ensure it fills the panel

                // Optional: If you want to clear previous controls, do so conditionally
                if (parentForm.changePassPanel.Controls.Count > 0)
                {
                    parentForm.changePassPanel.Controls.Clear(); 
                }

                // Add the ChangePassword form to the panel and display it
                parentForm.changePassPanel.Controls.Add(changePasswordForm);
                changePasswordForm.Show(); 
              
            }
            else
            {
                MessageBox.Show("Invalid verification code. Please try again.");
            }
        }

        private void verificaitoncode_Load(object sender, EventArgs e)
        {

        }
    }
}



