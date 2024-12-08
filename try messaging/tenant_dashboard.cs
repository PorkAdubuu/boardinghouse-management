using System;
using System.Data;
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
        private Timer notifCheckTimer;
        private bool hasNewNotification = false;


        public tenant_dashboard(int tenantId)
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#f7f7f7");
            this.tenantId = tenantId; // Store the tenant's ID
            InitializeMessageCheckTimer(); // Initialize the timer
            dbConnection = new DatabaseConnection();
            CheckForNewMessages();
            CheckNewNotifications();
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

            //notif timer 
            // Initialize and configure the timer
            notifCheckTimer = new Timer();
            notifCheckTimer.Interval = 3000; // Check every 30 seconds
            notifCheckTimer.Tick += NotifCheckTimer_Tick;
            notifCheckTimer.Start();




        }
        private void NotifCheckTimer_Tick(object sender, EventArgs e)
        {
            CheckNewNotifications();
        }

        private void CheckNewNotifications()
        {
            // Query to check if there are any new unread notifications for the tenant
            string query = @"
            SELECT COUNT(*) 
            FROM tenant_notif 
            WHERE tenant_id = @tenantId AND is_read = 0";  // Check for unread notifications (is_read = 0)

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tenantId", tenantId);

                int newNotifications = Convert.ToInt32(cmd.ExecuteScalar());

                // If there are new notifications, update the PictureBox image
                if (newNotifications > 0)
                {
                    if (!hasNewNotification)
                    {
                        notification_Btn.Image = Properties.Resources.notification__2_; // New notification image
                        hasNewNotification = true;
                    }
                }
                else
                {
                    if (hasNewNotification)
                    {
                        notification_Btn.Image = Properties.Resources.bell; // Default bell image
                        hasNewNotification = false;
                    }
                }
            }
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
        private void LoadNotifInPanel(Form childForm)
        {
            // Clear existing controls in the panel
            notifPanel.Controls.Clear();

            // Set up the child form's properties
            childForm.TopLevel = false; // Make it a non-top-level form
            childForm.FormBorderStyle = FormBorderStyle.None; // Remove the form border
            childForm.Dock = DockStyle.Fill; // Make it fill the panel

            // Add the form to the panel
            notifPanel.Controls.Add(childForm);
            notifPanel.Tag = childForm;

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

            LoadNotifications();


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

            notifPanel.Visible = false;





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

            notifPanel.Visible = false;
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

            notifPanel.Visible = false;
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

            notifPanel.Visible = false;
        }

        private void notification_Btn_Click(object sender, EventArgs e)
        {
            LoadNotifications();
            if (notifPanel.Visible)
            {
                // If the notification panel is visible, it means we are closing it
                // Mark notifications as read when the panel is being closed
                MarkNotificationsAsRead();
                
                // Reset notification icon to default bell image
                notification_Btn.Image = Properties.Resources.bell;

                // Hide the notification panel
                notifPanel.Visible = false;
            }
            else
            {
                // If the notification panel is not visible, we are opening it
                notifPanel.Visible = true;
                


            }

        }
        private void LoadNotifications()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
            SELECT 
                notif_id, 
                CONCAT(notif_type, ' | ', description, ' | ', DATE_FORMAT(notif_date, '%Y-%m-%d %h:%i %p')) AS Notification, 
                is_read
            FROM tenant_notif
            WHERE tenant_id = @tenantId
            ORDER BY notif_date DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable notificationsTable = new DataTable();
                    adapter.Fill(notificationsTable);

                    // Hide `notif_id` and `is_read` columns
                    notificationsTable.Columns.Remove("notif_id");

                    // Bind the DataTable to the DataGridView
                    this.notificationsTable.DataSource = notificationsTable;

                    // Deselect all rows after binding
                    this.notificationsTable.ClearSelection();

                    // Format the DataGridView
                    FormatDataGridView();

                    // Highlight unread notifications
                    HighlightUnreadRows();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading notifications: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void HighlightUnreadRows()
        {
            // Ensure DataGridView rows are loaded
            if (notificationsTable.Rows.Count == 0)
            {
                return; // No data, exit
            }

            foreach (DataGridViewRow row in notificationsTable.Rows)
            {
                if (row.Cells["is_read"].Value != DBNull.Value)
                {
                    try
                    {
                        int isRead = Convert.ToInt32(row.Cells["is_read"].Value);

                        // Debugging line to check values
                        Console.WriteLine($"Row ID: {row.Index}, is_read: {isRead}");

                        if (isRead == 0) // Unread notification
                        {
                            row.DefaultCellStyle.BackColor = Color.LightYellow; // Highlight unread notifications
                            
                        }
                        else // Read notification
                        {
                            row.DefaultCellStyle.BackColor = Color.White; // Default color
                            row.DefaultCellStyle.ForeColor = Color.Black; // Default text color for read notifications
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing row {row.Index}: {ex.Message}", "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Debugging line for null or invalid values
                    Console.WriteLine($"Row ID: {row.Index}, is_read value is NULL or invalid.");
                }
            }
        }

        private void FormatDataGridView()
        {
            // Hide the `is_read` column from being displayed
            if (notificationsTable.Columns.Contains("is_read"))
            {
                notificationsTable.Columns["is_read"].Visible = false;
            }

            // Set header for the Notification column
            notificationsTable.Columns["Notification"].HeaderText = "Notification Details";

            notificationsTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Automatically adjust column width
            notificationsTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;  // Automatically adjust row height
            notificationsTable.DefaultCellStyle.WrapMode = DataGridViewTriState.True;     // Enable text wrapping
            notificationsTable.AllowUserToAddRows = false;
            notificationsTable.ReadOnly = true;
            notificationsTable.RowHeadersVisible = false;
            notificationsTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            notificationsTable.MultiSelect = false;
            notificationsTable.DefaultCellStyle.SelectionBackColor = Color.LightYellow;
            notificationsTable.DefaultCellStyle.SelectionForeColor = notificationsTable.DefaultCellStyle.ForeColor;

            // Optional: Set font or alignment if needed
            notificationsTable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
        }

        private void MarkNotificationsAsRead()
        {
            string updateQuery = @"
            UPDATE tenant_notif 
            SET is_read = 1  -- Mark notifications as read
            WHERE tenant_id = @tenantId AND is_read = 0";  // Only update unread notifications (is_read = 0)

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@tenantId", tenantId);  // Pass the tenantId
                cmd.ExecuteNonQuery();
            }
        }


        private void tenant_dashboard_MouseClick(object sender, MouseEventArgs e)
        {
            // Hide the grid if it is visible and the click is outside the grid
            if (notifPanel.Visible)
            {
                // Check if the click was outside the grid
                if (!notifPanel.Bounds.Contains(e.Location))
                {
                    notifPanel.Visible = false;
                }
            }
        }

        private void notificationGrid_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void displayPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (notifPanel.Visible)
            {
                // Check if the click was outside the grid
                if (!notifPanel.Bounds.Contains(e.Location))
                {
                    notifPanel.Visible = false;
                }
            }
        }

        private void clear_Btn_Click(object sender, EventArgs e)
        {
            // Create a new database connection
            string connectionString = dbConnection.GetConnectionString(); // Replace with your actual connection string

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query to delete all records from the admin_notif table
                    string query = "DELETE FROM tenant_notif";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Execute the DELETE query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Notify the user of successful clearing
                        MessageBox.Show($"{rowsAffected} notifications cleared successfully.", "Notifications Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        notifPanel.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    MessageBox.Show("An error occurred while clearing notifications: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
