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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace try_messaging
{
    public partial class tenantmanagement : Form
    {
        public tenantmanagement()
        {
            InitializeComponent();
            this.CenterToScreen();
            // Set background color
            this.BackColor = ColorTranslator.FromHtml("#ffffff");
        }

        private async Task SendEmail(string toAddress, string subject, string body)
        {
            sendingLabel.Text = "Sending email...";
            sendingLabel.Visible = true;
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee; // Set to Marquee style for indefinite loading

            try
            {
                // Set the security protocol
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Use TLS 1.2

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)) // Use port 587
                {
                    smtpClient.Credentials = new NetworkCredential("boardinghouse24@gmail.com", "cjzvmzmwrspxxkxh"); // Email and app password
                    smtpClient.EnableSsl = true; // Enable SSL

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("boardinghouse24@gmail.com");
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = false; // HTML or plain text
                        mailMessage.To.Add(toAddress); // Add recipient email

                        // Send the email asynchronously
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }

                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.ToString()); // Detailed error message
            }
            finally
            {
                // Hide the ProgressBar and label after sending email
                progressBar.Visible = false;
                progressBar.Style = ProgressBarStyle.Blocks; // Reset to default style
                sendingLabel.Visible = false;
                MessageBox.Show("Email sent successfully!"); 
            }
        }

        // Method for generating a random password
        private string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder result = new StringBuilder();
            Random random = new Random();

            // Generate the password
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(validChars.Length);
                result.Append(validChars[index]);
            }

            return result.ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void tenantmanagement_Load(object sender, EventArgs e)
        {

        }

        private void emailgeneratorRich_TextChanged(object sender, EventArgs e)
        {

        }

        private async void sendbutton_Click(object sender, EventArgs e)
        {
            // Get data from text boxes and date picker
            string lastname = lnameText.Text;
            string firstname = fnameText.Text;
            int age = int.Parse(ageText.Text);
            int roomnumber = int.Parse(roomText.Text);
            string email = textBox1.Text;
            string username = usernameText.Text;
            string password = passwordText.Text;
            string contact = contactText.Text; // Get contact number from contactText
            string gender = genderCombo.SelectedItem.ToString(); // Get gender from genderCombo

            // Insert tenant into the database
            DatabaseConnection db = new DatabaseConnection();
            db.InsertTenant(lastname, firstname, age, roomnumber, email, username, password, contact, gender);

            // Prepare email content
            string tenantName = $"{firstname} {lastname}"; // Combine firstname and lastname for tenant's name
            string subject = "Welcome to the Boarding House Community!";
            string body = $"Dear {tenantName},\n\n" +
                          $"Welcome to the Boarding House Community!\n\n" +
                          $"We are pleased to inform you that your account has been successfully created. Below are your login credentials to access the Boarding House Management System:\n\n" +
                          $"Account Details:\n" +
                          $"Username: {username}\n" +
                          $"Password: {password}\n\n" +
                          $"**Notice:** These are temporary credentials. Please change your password after logging in.\n\n" + // Added notice
                          $"Login Instructions:\n" +
                          $"Visit the Boarding House Management System login Application:\n" +
                          $"Click on \"Login\" to access your account\n\n" +
                          $"Thank you for joining our community! We look forward to having you with us.\n\n" +
                          $"Best regards,\n" +
                          $"Your Boarding House Management Team";

            // Display the email content in emailgeneratorRich
            emailgeneratorRich.Text = $"To: {email}\nSubject: {subject}\n\n{body}";

            // Send the email
            await SendEmail(email, subject, body);
        }

        private void generateBtn_Click(object sender, EventArgs e)
        {
            string firstname = fnameText.Text;
            string lastname = lnameText.Text;

            // Generate the username based on nameText and roomText
            string username = $"{lastname}_{roomText.Text}";
            usernameText.Text = username;

            // Generate a random password
            string password = GenerateRandomPassword(12);
            passwordText.Text = password; // Set the generated password in the passwordText TextBox

            // Prepare email content to display in emailgeneratorRich
            string subject = "Welcome to the Boarding House Community!";
            string body = $"Dear {firstname} {lastname},\n\n" +
                          $"Welcome to the Boarding House Community!\n\n" +
                          $"We are pleased to inform you that your account has been successfully created. Below are your login credentials to access the Boarding House Management System:\n\n" +
                          $"Account Details:\n" +
                          $"Username: {username}\n" +
                          $"Password: {password}\n\n" +
                          $"**Notice:** These are temporary credentials. Please change your password after logging in.\n\n" +
                          $"Login Instructions:\n" +
                          $"Visit the Boarding House Management System login Application:\n" +
                          $"Click on \"Login\" to access your account\n\n" +
                          $"Thank you for joining our community! We look forward to having you with us.\n\n" +
                          $"Best regards,\n" +
                          $"Your Boarding House Management Team";

            // Display the email content in emailgeneratorRich
            emailgeneratorRich.Text = $"To: {textBox1.Text}\nSubject: {subject}\n\n{body}";
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            // Clear text boxes
            lnameText.Text = string.Empty;        // Last name
            fnameText.Text = string.Empty;        // First name
            ageText.Text = string.Empty;          // Age
            roomText.Text = string.Empty;         // Room number
            textBox1.Text = string.Empty;         // Email
            usernameText.Text = string.Empty;     // Username
            passwordText.Text = string.Empty;     // Password
            contactText.Text = string.Empty;      // Contact number

            // Reset the gender combo box to the default (first item, or you can set it to empty if you prefer)
            if (genderCombo.Items.Count > 0)
            {
                genderCombo.SelectedIndex = 0;    // Set to the first item
            }

            // Clear the email generator RichTextBox
            emailgeneratorRich.Text = string.Empty; // Clear email content display
        }
    }
}
