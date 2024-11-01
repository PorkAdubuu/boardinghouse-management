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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get data from text boxes and date picker
            string lastname = lnameText.Text; // Replace with your actual TextBox name
            string firstname = fnameText.Text; // Replace with your actual TextBox name
            int age = int.Parse(ageText.Text); // Make sure to validate the input
            DateTime birthdate = birthdatePicker.Value; // Replace with your actual DateTimePicker name
            int roomnumber = int.Parse(roomText.Text); // Make sure to validate the input
            string email = textBox1.Text; // Email TextBox
            string username = usernameText.Text; // Username TextBox
            string password = passwordText.Text; // Password TextBox

            // Insert tenant into the database
            DatabaseConnection db = new DatabaseConnection();
            db.InsertTenant(lastname, firstname, age, birthdate, roomnumber, email, username, password);

            // Send the email
            SendEmail(
                email,
                "Test Email",
                $"Hello,\n\nHere are your temporary credentials:\n\nUsername: {username}\nPassword: {password}\n\nPlease update your password after logging in.\n\nBest regards,\nYour Boarding House Management System"
            );
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
                MessageBox.Show("Error sending email: " + ex.ToString()); // Detailed error message
            }
        }


        
        //method for generating password
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
            // Generate the username based on nameText and roomText
            string username = $"{lnameText.Text}_{roomText.Text}";
            usernameText.Text = username; // Set the username in the usernameText TextBox
            passwordText.Text = GenerateRandomPassword(12);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            TenantLoginForm form2 = new TenantLoginForm();
            form2.Show();
            this.Hide();
        }
    }
}
