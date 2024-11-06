using System;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class tenant_dashboard : Form
    {
        private string verificationCode; // Define verificationCode at the class level
        private int tenantId; // Store the tenant's ID
        private DatabaseConnection dbConnection;
        private Timer messageCheckTimer;
        private Form parentForm;

        public tenant_dashboard(int tenantId)
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#f7f7f7");
            this.tenantId = tenantId; // Store the tenant's ID
            InitializeMessageCheckTimer(); // Initialize the timer
            dbConnection = new DatabaseConnection();
            CheckForNewMessages();
            LoadTenantName(); // Load the tenant's name when the form is created
            LoadTenantProfilePicture(tenantId);
            this.CenterToScreen();



            //textbox settings 
            tenantNameLabel.BorderStyle = BorderStyle.None;
            tenantNameLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#f7f7f7");
            tenantNameLabel.TextAlign = HorizontalAlignment.Right;
            this.Controls.Add(tenantNameLabel);

            

            this.ActiveControl = null;

            // Make sure the TextBox does not gain focus
            tenantNameLabel.TabStop = false;






        }

        private void LoadTenantProfilePicture(int tenantId)
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT profile_picture FROM tenants_details WHERE tenid = @tenantId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    byte[] imageData = cmd.ExecuteScalar() as byte[]; // ExecuteScalar returns the first column of the first row.

                    if (imageData != null)
                    {
                        // Convert the byte array into an Image and set it to the PictureBox
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            profilePic.Image = Image.FromStream(ms); // Set the image to the PictureBox
                        }
                    }
                    else
                    {
                        // If no image is found, you can set a default image or handle it accordingly
                        profilePic.Image = Properties.Resources.DefaultProfile; // Assuming you have a default image in Resources
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading profile picture: " + ex.Message);
                }
            }
        }


        private void LoadTenantName()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT firstname, lastname FROM tenants_details WHERE tenid = @tenantId", conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string firstName = reader["firstname"].ToString();
                        string lastName = reader["lastname"].ToString();

                        // Combine names and capitalize the first letter of each
                        string formattedName = $"{CapitalizeFirstLetter(lastName)} {CapitalizeFirstLetter(firstName)}";

                        // Set the formatted name to the adminNameLabel TextBox
                        tenantNameLabel.Text = formattedName;
                    }
                    else
                    {
                        tenantNameLabel.Text = "Tenant Not Found"; // Handle case where tenant is not found
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tenant name: " + ex.Message);
                }
            }
        }

        private string CapitalizeFirstLetter(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            // Capitalize the first letter and return the rest unchanged
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }
        private void InitializeMessageCheckTimer()
        {
            messageCheckTimer = new Timer();
            messageCheckTimer.Interval = 3000; // Set interval to 3 seconds
            messageCheckTimer.Tick += MessageCheckTimer_Tick; // Attach the tick event handler
            messageCheckTimer.Start(); // Start the timer
        }
        private void MessageCheckTimer_Tick(object sender, EventArgs e)
        {
            CheckForNewMessages(); // Check for new messages every tick
        }
        private void CheckForNewMessages()
        {
            bool hasNewMessages = false;

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString())) // Use the database connection
            {
                try
                {
                    conn.Open();
                    // Check for unread messages
                    MySqlCommand cmd = new MySqlCommand(
                        "SELECT COUNT(*) FROM combined_messages WHERE sender_type = 'admin' AND is_read =0", conn);

                    int unreadCount = Convert.ToInt32(cmd.ExecuteScalar());
                    hasNewMessages = unreadCount > 0; // Set to true if there are unread messages
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking for new messages: " + ex.Message);
                }
            }

            // Change the image in the PictureBox based on whether there are new messages
            if (hasNewMessages)
            {
                mail_icon.Image = Properties.Resources.mail_notif; // Set to the notification icon
            }
            else
            {
                mail_icon.Image = Properties.Resources.mail_default; // Set to the default icon
            }
        }

        private void LoadFormInPanel(Form childForm)
        {
            // Clear existing controls in the panel
            displayPanel.Controls.Clear();

            // Set up the child form's properties
            childForm.TopLevel = false; // Make it a non-top-level form
            childForm.FormBorderStyle = FormBorderStyle.None; // Remove the form border
            childForm.Dock = DockStyle.Fill; // Make it fill the panel

            // Add the form to the panel
            displayPanel.Controls.Add(childForm);
            displayPanel.Tag = childForm;

            // Display the form
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            changepassword form4 = new changepassword(tenantId);
            LoadFormInPanel(form4); // Load changepassword form in displayPanel
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tenantcomform tenantcomform = new tenantcomform(tenantId);
            LoadFormInPanel(tenantcomform); // Load tenantcomform form in displayPanel
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mail_icon_Click(object sender, EventArgs e)
        {
            tenantcomform tenantcomform = new tenantcomform(tenantId);
            LoadFormInPanel(tenantcomform);
            MarkMessagesAsRead();
        }
        private void MarkMessagesAsRead()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    
                    MySqlCommand cmd = new MySqlCommand(
                        "UPDATE combined_messages SET is_read = 1 WHERE sender_type = 'admin' AND is_read = 0", conn);
                    cmd.ExecuteNonQuery(); // Execute the update command
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating message status: " + ex.Message);
                }
            }
        }

        private void profilePic_Click(object sender, EventArgs e)
        {
            tenant_profile tenantProfile = new tenant_profile(verificationCode,tenantId); // Passing 'this' to tenant_profile
            LoadFormInPanel(tenantProfile);
        }

        
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TenantLoginForm tenantLoginForm1 = new TenantLoginForm();
            tenantLoginForm1.Show();
            this.Close();
        }
    }
}
