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
        private Timer clockTimer;


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

            //load dashboard display
            tenant_dashboard_display tenant_Dashboard_Display = new tenant_dashboard_display(tenantId);
            LoadFormInPanel(tenant_Dashboard_Display);


            dashboard_Btn.Click += dashboard_Btn_Click_1;
            profile_Btn.Click += profile_Btn_Click;
            payment_Btn.Click += Payment_Btn_Click;
            maintenance_Btn.Click += maintenance_Btn_Click;

            //timee
            // Initialize the Timer
            clockTimer = new Timer();
            clockTimer.Interval = 1000; // 1 second
            clockTimer.Tick += ClockTimer_Tick;

            // Start the Timer
            clockTimer.Start();

            timeLabel.Text = DateTime.Now.ToString("MMMM dd, yyyy | hh:mm:ss tt");




            //textbox settings 
            tenantNameLabel.BorderStyle = BorderStyle.None;
            tenantNameLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#f7f7f7");
            tenantNameLabel.TextAlign = HorizontalAlignment.Right;
            this.Controls.Add(tenantNameLabel);

            

            this.ActiveControl = null;

            // Make sure the TextBox does not gain focus
            tenantNameLabel.TabStop = false;






        }
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            // Update the label with the current time
            timeLabel.Text = DateTime.Now.ToString("MMMM dd, yyyy | hh:mm:ss tt");
        }

        private void Payment_Btn_Click(object sender, EventArgs e)
        {
           
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
                mail_icon.Image = Properties.Resources.communication; // Set to the notification icon
            }
            else
            {
                mail_icon.Image = Properties.Resources.mail; // Set to the default icon
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
            dashboard_Btn_Click_1(dashboard_Btn, EventArgs.Empty);


        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mail_icon_Click(object sender, EventArgs e)
        {
            
            
            tenantcomform tenantcomform = new tenantcomform(tenantId);
            tenantcomform.Show();

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

            // Change the image of the profile button
            

            // Reset the dashboard button to its default image
            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            payment_Btn.Image = Properties.Resources.payment_plain_butt;
            maintenance_Btn.Image = Properties.Resources.maintenance_plain_butt_11;
            profile_Btn.Image = Properties.Resources.profile_plain_butt__1_;
        }

        
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           startingwindow startingwindow = new startingwindow();
            startingwindow.Show();
            this.Close();
        }



        private void dashboard_Btn_Click_1(object sender, EventArgs e)
        {
            
            // Clear the panel and load the dashboard form
            tenant_dashboard_display tenant_Dashboard_Display = new tenant_dashboard_display(tenantId);
            LoadFormInPanel(tenant_Dashboard_Display);

            // Change the image of the dashboard button
            dashboard_Btn.Image = Properties.Resources.dashboard_pink_butt;

            // Reset the profile button to its default image
            profile_Btn.Image = Properties.Resources.profile_plain_butt__1_;
            payment_Btn.Image = Properties.Resources.payment_plain_butt;
            maintenance_Btn.Image = Properties.Resources.maintenance_plain_butt_11;

            notificationGrid.Visible = false;





        }


        private void profile_Btn_Click(object sender, EventArgs e)
        {
            displayPanel.Controls.Clear();
            // Clear the panel and load the profile form
            tenant_account_profile tenant_Account_Profile = new tenant_account_profile(tenantId);
            LoadFormInPanel(tenant_Account_Profile);

            // Change the image of the profile button
            profile_Btn.Image = Properties.Resources.profile_pink_butt__1_;

            // Reset the dashboard button to its default image
            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            payment_Btn.Image = Properties.Resources.payment_plain_butt;
            maintenance_Btn.Image = Properties.Resources.maintenance_plain_butt_11;

            notificationGrid.Visible = false;
        }

        private void payment_Btn_Click(object sender, EventArgs e)
        {
            // Clear the panel and load the profile form
            tenant_payment tenant_Pyament = new tenant_payment(tenantId);
            LoadFormInPanel(tenant_Pyament);    

            //code here the target form
            

            // Change the image of the profile button
            payment_Btn.Image = Properties.Resources.payment_pink_butt;

            // Reset the dashboard button to its default image
            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            profile_Btn.Image = Properties.Resources.profile_plain_butt__1_;
            maintenance_Btn.Image = Properties.Resources.maintenance_plain_butt_11;

            notificationGrid.Visible = false;
        }

        private void maintenance_Btn_Click(object sender, EventArgs e)
        {
            // Clear the panel and load the profile form
            

            //code here the target form


            // Change the image of the profile button
            maintenance_Btn.Image = Properties.Resources.maintenance_pink_butt;

            // Reset the dashboard button to its default image
            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            profile_Btn.Image = Properties.Resources.profile_plain_butt__1_;
            payment_Btn.Image = Properties.Resources.payment_plain_butt;

            notificationGrid.Visible = false;
        }

        private void notification_Btn_Click(object sender, EventArgs e)
        {
            notificationGrid.Visible = !notificationGrid.Visible;
        }

        private void tenant_dashboard_MouseClick(object sender, MouseEventArgs e)
        {
            // Hide the grid if it is visible and the click is outside the grid
            if (notificationGrid.Visible)
            {
                // Check if the click was outside the grid
                if (!notificationGrid.Bounds.Contains(e.Location))
                {
                    notificationGrid.Visible = false;
                }
            }
        }

        private void notificationGrid_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void displayPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (notificationGrid.Visible)
            {
                // Check if the click was outside the grid
                if (!notificationGrid.Bounds.Contains(e.Location))
                {
                    notificationGrid.Visible = false;
                }
            }
        }
    }
}
