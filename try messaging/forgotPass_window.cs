using System;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.ComponentModel;

namespace try_messaging
{
    public partial class forgotPass_window : Form
    {
        private DatabaseConnection dbConnection;
        private string verificationCode; // Store the generated verification code
        private BackgroundWorker emailWorker; // Background worker for sending email

        public forgotPass_window()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
            this.CenterToParent();

            // Initialize BackgroundWorker
            emailWorker = new BackgroundWorker();
            emailWorker.DoWork += EmailWorker_DoWork;
            emailWorker.RunWorkerCompleted += EmailWorker_RunWorkerCompleted;
        }

        private void sendcode_Btn_Click(object sender, EventArgs e)
        {
            string enteredEmail = emailText.Text.Trim(); // Get the email entered by the user

            if (string.IsNullOrEmpty(enteredEmail))
            {
                MessageBox.Show("Please enter your email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the email exists in the tenants or admin database
            if (!IsEmailRegistered(enteredEmail))
            {
                MessageBox.Show("The entered email address is not registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Generate the verification code
            verificationCode = GenerateVerificationCode();

            // Start the background worker to send email
            emailWorker.RunWorkerAsync(enteredEmail);
        }

        private void forgotPass_window_Load(object sender, EventArgs e)
        {
            // Optional: Any initialization code here
        }

        private void EmailWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string email = (string)e.Argument;
            SendEmail(email, "Password Reset Verification Code", $"Your verification code is: {verificationCode}");
        }

        private void EmailWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // Hide the ProgressBar and label after sending email
            progressBar.Visible = false;
            progressBar.Style = ProgressBarStyle.Blocks; // Reset to default style
            sendingLabel.Visible = false;

            if (e.Error != null)
            {
                MessageBox.Show("Error sending email: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Show a single success message
            MessageBox.Show("A 6-digit verification code has been sent to your email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Determine if the email is for a tenant or admin and open the corresponding reset form
            string enteredEmail = emailText.Text.Trim();
            if (IsTenantEmail(enteredEmail))
            {
                resetPassword_code resetPasswordCodeForm = new resetPassword_code(verificationCode, enteredEmail);
                resetPasswordCodeForm.Show();
            }
            else
            {
                resetPassword_code_admin resetPasswordCodeFormAdmin = new resetPassword_code_admin(verificationCode, enteredEmail);
                resetPasswordCodeFormAdmin.Show();
            }

            this.Close();
        }


        private void SendEmail(string toAddress, string subject, string body)
        {
            // Ensure UI updates are done on the main thread
            if (sendingLabel.InvokeRequired)
            {
                sendingLabel.Invoke(new Action(() =>
                {
                    sendingLabel.Text = "Sending email...";
                    sendingLabel.Visible = true;
                    progressBar.Visible = true;
                    progressBar.Style = ProgressBarStyle.Marquee; // Set to Marquee style for indefinite loading
                }));
            }
            else
            {
                sendingLabel.Text = "Sending email...";
                sendingLabel.Visible = true;
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
            }

            try
            {
                // Set the security protocol
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("boardinghouse24@gmail.com", "cjzvmzmwrspxxkxh"); // Your email and app password
                    smtpClient.EnableSsl = true;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("boardinghouse24@gmail.com");
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = false;
                        mailMessage.To.Add(toAddress);

                        // Send the email
                        smtpClient.Send(mailMessage);
                    }
                }

                // After email is sent, the UI will be updated in the RunWorkerCompleted event, so no need for another MessageBox here
            }
            catch (Exception ex)
            {
                // If error occurs, ensure the UI updates are on the main thread
                if (sendingLabel.InvokeRequired)
                {
                    sendingLabel.Invoke(new Action(() =>
                    {
                        MessageBox.Show("Error sending email: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
                else
                {
                    MessageBox.Show("Error sending email: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Generate a random 6-digit code
        }

        private bool IsEmailRegistered(string email)
        {
            // Check if email exists in either tenants or admin tables
            return IsTenantEmail(email) || IsAdminEmail(email);
        }

        private bool IsTenantEmail(string email)
        {
            string query = "SELECT COUNT(*) FROM tenants_details WHERE email = @Email";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        int emailCount = Convert.ToInt32(cmd.ExecuteScalar());
                        return emailCount > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking tenant email in the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private bool IsAdminEmail(string email)
        {
            string query = "SELECT COUNT(*) FROM admin_details WHERE email = @Email";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        int emailCount = Convert.ToInt32(cmd.ExecuteScalar());
                        return emailCount > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking admin email in the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
