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
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using DocumentFormat.OpenXml.Wordprocessing;

namespace try_messaging
{
    public partial class tenantmanagement : Form
    {
        private DatabaseConnection dbConnection;
        private int adminID;
        private int admin_Id;
        private string aircondition = "NO";
        private string wifii = "NO";
        private string parkingg = "NO";
        public tenantmanagement(int adminID)
        {
            InitializeComponent();
            this.CenterToScreen();
            // Set background color
            this.BackColor = ColorTranslator.FromHtml("#f7f7f7");
            this.adminID = adminID;
            this.admin_Id = admin_Id;
            dbConnection = new DatabaseConnection();



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
            genderCombo.SelectedIndex = 0;

            PopulateAvailableRooms();
            PopulateBoardingHouses();
        }

        private void emailgeneratorRich_TextChanged(object sender, EventArgs e)
        {

        }
        private bool ValidateFields()
        {
            // Check if any of the required fields are empty or not selected
            if (string.IsNullOrEmpty(lnameText.Text) ||
                string.IsNullOrEmpty(fnameText.Text) ||
                string.IsNullOrEmpty(ageText.Text) ||
                string.IsNullOrEmpty(roomText.Text) ||
                string.IsNullOrEmpty(boardingCombo.Text) ||
                string.IsNullOrEmpty(textBox1.Text) ||    // Email field
                string.IsNullOrEmpty(usernameText.Text) ||
                string.IsNullOrEmpty(passwordText.Text) ||
                string.IsNullOrEmpty(contactText.Text) ||
                genderCombo.SelectedItem == null ||        // Gender combo box
                string.IsNullOrEmpty(addText.Text) ||      // Address field
                string.IsNullOrEmpty(emergency_contactt1.Text) ||
                string.IsNullOrEmpty(emergency_number1.Text) ||
                movein_datapicker.Value == DateTime.MinValue ||
                expiration_datapicker.Value == DateTime.MinValue)
            {
                return false; // Validation failed
            }
            return true; // All fields are filled
        }

        private async void sendbutton_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) // Check if validation fails
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Prevent the email from being sent if validation fails
            }
            string email = textBox1.Text;

            // Validate email format
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop execution if email is invalid
            }
            string selectedBoardingHouse = boardingCombo.Text;

            // Check boarding house availability
            DatabaseConnection db = new DatabaseConnection();
            if (!db.IsBoardingHouseAvailable(selectedBoardingHouse))
            {
                MessageBox.Show("The selected boarding house has reached its capacity. Please choose a different house.", "Capacity Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop execution if capacity is full
            }

            // Get data from text boxes
            string lastname = lnameText.Text.ToUpper();
            string firstname = fnameText.Text.ToUpper();
            int age = int.Parse(ageText.Text);
            int roomnumber = int.Parse(roomText.Text);
            email = textBox1.Text;
            string username = usernameText.Text;
            string password = passwordText.Text;
            string contact = contactText.Text;
            string gender = genderCombo.SelectedItem.ToString().ToUpper();
            string address = addText.Text.ToUpper();
            string emergency_name1 = emergency_contactt1.Text.ToUpper();
            string emergency_name2 = emergency_contactt2.Text.ToUpper();
            string emergency_contact1 = emergency_number1.Text;
            string emergency_contact2 = emergency_number2.Text;
            int pax_number = int.Parse(paxText.Text);

            string wifi = wifii;
            string parking = parkingg;
            DateTime movein_date = movein_datapicker.Value;
            DateTime expiration_date = expiration_datapicker.Value;
            DateTime birth_date = birthDatePicker.Value;

            // Create a database connection
            DatabaseConnection dbase = new DatabaseConnection();

            // Check if email or contact already exists
            if (db.IsEmailOrContactExists(email, contact))
            {
                MessageBox.Show("The email or contact number is already in use. Please use different values.",
                                "Duplicate Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop execution if duplicate data is found
            }

            // Insert tenant into the database
            db.InsertTenant(lastname, firstname, age, roomnumber, email, username, password, contact, gender, address, emergency_name1, emergency_name2, emergency_contact1, emergency_contact2, wifi, parking, movein_date, expiration_date, selectedBoardingHouse, birth_date, pax_number);

            db.UpdateCurrentOccupancy(selectedBoardingHouse);

            // Create the PDF with credentials
            byte[] pdfData = CreatePdfWithCredentials(firstname, lastname, username, password);

            // Send the email with the PDF attachment
            await SendEmailWithPdfAttachment(email, pdfData);

            PopulateAvailableRooms();
        }
        private byte[] CreatePdfWithCredentials(string firstname, string lastname, string username, string password)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Create a new PDF document
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();

                // Create a graphics object to write on the PDF page
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Define fonts
                XFont headerFont = new XFont("Verdana", 14, XFontStyleEx.Bold);
                XFont bodyFont = new XFont("Verdana", 12);

                // Write text to the document
                gfx.DrawString("Welcome to the Boarding House Community!", headerFont, XBrushes.Black, new XPoint(40, 40));
                gfx.DrawString($"Dear {firstname} {lastname},", bodyFont, XBrushes.Black, new XPoint(40, 80));
                gfx.DrawString("We are pleased to inform you that your account has been successfully created.", bodyFont, XBrushes.Black, new XPoint(40, 120));
                gfx.DrawString($"Below are your login credentials:", bodyFont, XBrushes.Black, new XPoint(40, 135));
                gfx.DrawString($"Username: {username}", bodyFont, XBrushes.Black, new XPoint(40, 160));
                gfx.DrawString($"Password: {password}", bodyFont, XBrushes.Black, new XPoint(40, 180));
                gfx.DrawString("*Notice:* These are temporary credentials. Please change your password after logging", bodyFont, XBrushes.Black, new XPoint(40, 220));
                gfx.DrawString("in.", bodyFont, XBrushes.Black, new XPoint(40, 235));
                gfx.DrawString("Thank you for joining our community!", bodyFont, XBrushes.Black, new XPoint(40, 250));
                gfx.DrawString("Best regards,", bodyFont, XBrushes.Black, new XPoint(40, 265));
                gfx.DrawString("Your Boarding House Management Team", bodyFont, XBrushes.Black, new XPoint(40, 280));

                // Save the document to the memory stream
                document.Save(ms);

                // Return the PDF as a byte array
                return ms.ToArray();
            }
        }

        private async Task SendEmailWithPdfAttachment(string toAddress, byte[] pdfData)
        {
            sendingLabel.Text = "Sending email...";
            sendingLabel.Visible = true;
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee; // Set to Marquee style for indefinite loading
            string firstname = fnameText.Text;
            string lastname = lnameText.Text;

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
                        mailMessage.Subject = "Welcome to the Boarding House Community!";
                        mailMessage.IsBodyHtml = true; // Enable HTML content for a more styled email body

                        // Create a formal introduction without including credentials in the email body
                        mailMessage.Body = @"
                        <html>
                        <body>
                            <h2>Welcome to BoardMate!</h2>
                            <p>Dear " + firstname + " " + lastname + @",</p>
                            <p>We are pleased to welcome you to the BoardMate community. Your registration has been successfully completed, and your account credentials have been generated.</p>
                            <p>Please find your temporary credentials in the attached PDF document. Kindly remember to change your password after your first login for security purposes.</p>
                            <p>If you have any questions or need assistance, feel free to contact our support team.</p>
                            <p>Thank you for choosing BoardMate, and we look forward to having you as part of our community!</p>
                            <p><strong>Best regards,</strong><br />
                            BoardMate Management Team</p>
                        </body>
                        </html>";

                        // Send the email to the recipient
                        mailMessage.To.Add(toAddress);

                        // Create the attachment with the PDF data
                        MemoryStream ms = new MemoryStream(pdfData);
                        Attachment attachment = new Attachment(ms, "boarding_house_credentials.pdf", "application/pdf");
                        mailMessage.Attachments.Add(attachment);

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
                ClearFields();
                PopulateAvailableRooms();
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                // Use Regex to validate email format
                var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return emailRegex.IsMatch(email);
            }
            catch
            {
                return false; // Return false if any exception occurs
            }
        }

        public bool IsEmailOrContactExists(string email, string contact)
        {
            bool exists = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection("YourConnectionStringHere"))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM tenants_details WHERE email = @Email OR contact = @Contact";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Contact", contact);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        exists = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking duplicates: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return exists;
        }



        private void PopulateAvailableRooms()
        {
            if (string.IsNullOrEmpty(boardingCombo.Text))
            {
                roomText.DataSource = null; // Clear roomText if no house is selected
                return;
            }

            string selectedBoardingHouse = boardingCombo.Text;

            // Define room ranges based on boarding house capacity
            List<int> allRooms = new List<int>();
            int capacity = GetBoardingHouseCapacity(selectedBoardingHouse); // Get the capacity of the selected boarding house

            if (capacity == 10)
            {
                // For capacity 10, rooms are 101-105 on the first floor, and 201-205 on the second floor
                List<Tuple<int, int>> roomRanges = new List<Tuple<int, int>>
        {
            new Tuple<int, int>(101, 105), // First floor (101-105)
            new Tuple<int, int>(201, 205)  // Second floor (201-205)
        };

                foreach (var range in roomRanges)
                {
                    for (int room = range.Item1; room <= range.Item2; room++)
                    {
                        allRooms.Add(room);
                    }
                }
            }
            else if (capacity == 20)
            {
                // For capacity 20, rooms are 101-110 on the first floor, and 201-210 on the second floor
                List<Tuple<int, int>> roomRanges = new List<Tuple<int, int>>
        {
            new Tuple<int, int>(101, 110), // First floor (101-110)
            new Tuple<int, int>(201, 210)  // Second floor (201-210)
        };

                foreach (var range in roomRanges)
                {
                    for (int room = range.Item1; room <= range.Item2; room++)
                    {
                        allRooms.Add(room);
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid capacity for the selected boarding house.");
                return;
            }

            // Query the database for taken rooms for the selected house
            List<int> takenRooms = new List<int>();
            string query = "SELECT roomnumber FROM tenants_details WHERE house_name = @HouseName";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@HouseName", selectedBoardingHouse);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        takenRooms.Add(reader.GetInt32("roomnumber"));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving taken rooms: {ex.Message}");
                }
            }

            // Filter available rooms by excluding taken rooms
            List<int> availableRooms = allRooms.Except(takenRooms).ToList();

            // Bind available rooms to the ComboBox
            roomText.DataSource = availableRooms;

            if (availableRooms.Count == 0)
            {
                MessageBox.Show($"No rooms are available for {selectedBoardingHouse}.", "No Rooms Available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // This function retrieves the capacity of the selected boarding house from the database
        private int GetBoardingHouseCapacity(string houseName)
        {
            int capacity = 0;
            string query = "SELECT capacity FROM boarding_houses WHERE house_name = @HouseName";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@HouseName", houseName);
                    capacity = Convert.ToInt32(cmd.ExecuteScalar()); // Get capacity from the database
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving boarding house capacity: {ex.Message}");
                }
            }

            return capacity;
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
            string subject = "Welcome to BoardMate!";
            string body = $"Dear {firstname} {lastname},\n\n" +
                          $"Welcome to the Boarding House Community!\n\n" +
                          $"We are pleased to welcome you to the BoardMate community. Your registration has been successfully completed, and your account credentials have been generated.\n\n" +                       
                          $"Please find your temporary credentials in the attached PDF document. Kindly remember to change your password after your first login for security purposes.\n\n" +                          
                          $"Login Instructions:\n" +
                          $"Visit the Boarding House Management System login Application:\n" +
                          $"Click on \"Login\" to access your account\n\n" +
                          $"If you have any questions or need assistance, feel free to contact our support team.\n\n" +
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
            lnameText.Text = string.Empty;
            fnameText.Text = string.Empty;
            ageText.Text = string.Empty;
            roomText.Text = string.Empty;
            textBox1.Text = string.Empty;
            usernameText.Text = string.Empty;
            passwordText.Text = string.Empty;
            contactText.Text = string.Empty;
            addText.Text = string.Empty; // Added for address field
            emergency_contactt1.Text = string.Empty; // Added for emergency contact 1 name
            emergency_contactt2.Text = string.Empty; // Added for emergency contact 2 name
            emergency_number1.Text = string.Empty; // Added for emergency contact 1 number
            emergency_number2.Text = string.Empty; // Added for emergency contact 2 number    
            movein_datapicker.Value = DateTime.Now; // Reset to current date
            expiration_datapicker.Value = DateTime.Now; // Reset to current date

            if (genderCombo.Items.Count > 0)
            {
                genderCombo.SelectedIndex = 0;    
            }
            
            // Clear the email generator RichTextBox
            emailgeneratorRich.Text = string.Empty; 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            admin_dashboard admindashboard = new admin_dashboard(adminID);
            admindashboard.Show();
            this.Hide();
        }

        

        private void PopulateBoardingHouses()
        {
            DatabaseConnection db = new DatabaseConnection();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(db.GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT house_name FROM boarding_houses";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        List<string> houseNames = new List<string>();

                        while (reader.Read())
                        {
                            houseNames.Add(reader.GetString("house_name"));
                        }

                        boardingCombo.DataSource = houseNames;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating boarding houses: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        

        private void amenities2_CheckedChanged(object sender, EventArgs e)
        {
            wifii = amenities2.Checked ? "YES" : "NO";
        }

        private void amenities3_CheckedChanged(object sender, EventArgs e)
        {
            parkingg = amenities3.Checked ? "YES" : "NO";
        }

        private void boardingCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateAvailableRooms();
        }

        private void birthDatePicker_ValueChanged(object sender, EventArgs e)
        {
            // Get the selected date
            DateTime selectedDate = birthDatePicker.Value;

            // Calculate the age
            int age = CalculateAge(selectedDate);

            // Display the age in the textbox
            ageText.Text = age.ToString();
        }
        private int CalculateAge(DateTime birthDate)
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Calculate the age
            int age = today.Year - birthDate.Year;

            // Adjust the age if the birthday hasn't occurred yet this year
            if (birthDate.Date > today.AddYears(-age)) age--;

            return age;
        }

        private void generate_Btn1_Click(object sender, EventArgs e)
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
            string subject = "Welcome to BoardMate!";
            string body = $"Dear {firstname} {lastname},\n\n" +
                          $"Welcome to the Boarding House Community!\n\n" +
                          $"We are pleased to welcome you to the BoardMate community. Your registration has been successfully completed, and your account credentials have been generated.\n\n" +
                          $"Please find your temporary credentials in the attached PDF document. Kindly remember to change your password after your first login for security purposes.\n\n" +
                          $"Login Instructions:\n" +
                          $"Visit the Boarding House Management System login Application:\n" +
                          $"Click on \"Login\" to access your account\n\n" +
                          $"If you have any questions or need assistance, feel free to contact our support team.\n\n" +
                          $"Thank you for joining our community! We look forward to having you with us.\n\n" +
                          $"Best regards,\n" +
                          $"Your Boarding House Management Team";

            // Display the email content in emailgeneratorRich
            emailgeneratorRich.Text = $"To: {textBox1.Text}\nSubject: {subject}\n\n{body}";
        }

        private void cancel_Btn1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private async void encode_Btn1_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) // Check if validation fails
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Prevent the email from being sent if validation fails
            }
            string email = textBox1.Text;

            // Validate email format
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop execution if email is invalid
            }
            string selectedBoardingHouse = boardingCombo.Text;

            // Check boarding house availability
            DatabaseConnection db = new DatabaseConnection();
            if (!db.IsBoardingHouseAvailable(selectedBoardingHouse))
            {
                MessageBox.Show("The selected boarding house has reached its capacity. Please choose a different house.", "Capacity Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop execution if capacity is full
            }

            // Get data from text boxes
            string lastname = lnameText.Text.ToUpper();
            string firstname = fnameText.Text.ToUpper();
            int age = int.Parse(ageText.Text);
            int roomnumber = int.Parse(roomText.Text);
            email = textBox1.Text;
            string username = usernameText.Text;
            string password = passwordText.Text;
            string contact = contactText.Text;
            string gender = genderCombo.SelectedItem.ToString().ToUpper();
            string address = addText.Text.ToUpper();
            string emergency_name1 = emergency_contactt1.Text.ToUpper();
            string emergency_name2 = emergency_contactt2.Text.ToUpper();
            string emergency_contact1 = emergency_number1.Text;
            string emergency_contact2 = emergency_number2.Text;
            int pax_number = int.Parse(paxText.Text);

            string wifi = wifii;
            string parking = parkingg;
            DateTime movein_date = movein_datapicker.Value;
            DateTime expiration_date = expiration_datapicker.Value;
            DateTime birth_date = birthDatePicker.Value;

            // Create a database connection
            DatabaseConnection dbase = new DatabaseConnection();

            // Check if email or contact already exists
            if (db.IsEmailOrContactExists(email, contact))
            {
                MessageBox.Show("The email or contact number is already in use. Please use different values.",
                                "Duplicate Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop execution if duplicate data is found
            }

            // Insert tenant into the database
            db.InsertTenant(lastname, firstname, age, roomnumber, email, username, password, contact, gender, address, emergency_name1, emergency_name2, emergency_contact1, emergency_contact2, wifi, parking, movein_date, expiration_date, selectedBoardingHouse, birth_date, pax_number);

            db.UpdateCurrentOccupancy(selectedBoardingHouse);

            // Create the PDF with credentials
            byte[] pdfData = CreatePdfWithCredentials(firstname, lastname, username, password);

            // Send the email with the PDF attachment
            await SendEmailWithPdfAttachment(email, pdfData);

            PopulateAvailableRooms();
        }
    }
}
