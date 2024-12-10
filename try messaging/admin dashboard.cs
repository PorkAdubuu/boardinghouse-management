using MySql.Data.MySqlClient; // Ensure this namespace is included
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data;


namespace try_messaging
{
    public partial class admin_dashboard : Form
    {
        private Timer messageCheckTimer; // Timer to check for new messages
        private DatabaseConnection dbConnection;
        private int adminId;
        private Timer clockTimer;
        private string verificationCode; // To store the verification code

        private bool hasNewNotification = false;
        private Timer notifCheckTimer;

        public admin_dashboard(int adminId)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.adminId = adminId;

            this.BackColor = ColorTranslator.FromHtml("#f7f7f7");
            dbConnection = new DatabaseConnection(); // Initialize the DatabaseConnection
            InitializeMessageCheckTimer(); // Initialize the timer
            CheckForNewMessages();
            LoadAdminName();


            adminNameLabel.BorderStyle = BorderStyle.None;
            adminNameLabel.BackColor = System.Drawing.ColorTranslator.FromHtml("#f7f7f7");
            adminNameLabel.TextAlign = HorizontalAlignment.Right;
            this.Controls.Add(adminNameLabel);

            this.ActiveControl = null;

            // Make sure the TextBox does not gain focus
            adminNameLabel.TabStop = false;

            //timee
            // Initialize the Timer
            clockTimer = new Timer();
            clockTimer.Interval = 1000; // 1 second
            clockTimer.Tick += ClockTimer_Tick;

            // Start the Timer
            clockTimer.Start();

            timeLabel.Text = DateTime.Now.ToString("MMMM dd, yyyy | hh:mm:ss tt");

            admin_dashboard_display admin_Dashboard_Display = new admin_dashboard_display();
            LoadFormInPanel(admin_Dashboard_Display);

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
            FROM admin_notif 
            WHERE is_read = 0";  // Check for unread notifications (is_read = 0)

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);               

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
        private void LoadAdminName()
        {
            string adminName = dbConnection.GetAdminName(adminId);
            if (!string.IsNullOrEmpty(adminName))
            {
                adminNameLabel.Text = adminName; // Display the name in the label
            }
            else
            {
                MessageBox.Show("Failed to load admin name.");
            }
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
                        "SELECT COUNT(*) FROM combined_messages WHERE sender_type = 'tenant' AND is_read = 0", conn);

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

        private void admin_dashboard_Load(object sender, EventArgs e)
        {
            // Initial load actions can be performed here if needed
            dashboard_Btn_Click(dashboard_Btn, EventArgs.Empty);
            LoadAdminProfilePicture(adminId);

            CheckNewNotifications();
            LoadNotifications();
        }

        private void LoadAdminProfilePicture(int adminId)
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT profile_picture FROM admin_details WHERE admin_details_id = @adminId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@adminId", adminId);

                    byte[] imageData = cmd.ExecuteScalar() as byte[];

                    if (imageData != null && imageData.Length > 0)
                    {
                        // Convert byte array to image and display in PictureBox
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            profile_picture.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        // If no image found, use default image
                        profile_picture.Image = Properties.Resources.DefaultProfile;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading profile picture: " + ex.Message);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            tenantmanagement tenantmanagement = new tenantmanagement(adminId);
            tenantmanagement.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            admincomform admincomform = new admincomform();
            LoadFormInPanel(admincomform);
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {
            // Painting logic if needed
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Logic for picture box 2 click
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Logic for picture box 4 click
        }

        private void mail_icon_Click(object sender, EventArgs e)
        {
            admincomform admincomform1 = new admincomform();
            LoadFormInPanel(admincomform1);

            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            managetenant_Btn.Image = Properties.Resources.admin_manage_tenant_plain_butt;
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt__1_;
            payments_Btn.Image = Properties.Resources.admin_tenant_payment_plain_butt;
            maintenance_Btn.Image = Properties.Resources.admin_maintenance_plain_butt;
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_plain_butt;

        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            startingwindow adminLoginForm = new startingwindow();
            adminLoginForm.Show();
            this.Close();
        }

        private void dashboard_Btn_Click(object sender, EventArgs e)
        {
            displayPanel.Controls.Clear();
            // Clear the panel and load the dashboard form
            admin_dashboard_display admin_Dashboard_Display = new admin_dashboard_display();
            LoadFormInPanel(admin_Dashboard_Display);
            // Change the image of the dashboard button
            dashboard_Btn.Image = Properties.Resources.dashboard_pink_butt;

            //plain 
            managetenant_Btn.Image = Properties.Resources.admin_manage_tenant_plain_butt;
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt__1_;
            payments_Btn.Image = Properties.Resources.admin_tenant_payment_plain_butt;
            maintenance_Btn.Image = Properties.Resources.admin_maintenance_plain_butt;
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_plain_butt;
            //notif out 
            notifPanel.Visible = false;
        }

        private void managetenant_Btn_Click(object sender, EventArgs e)
        {
            tenantmanagementPanel tenantmanagementPanel = new tenantmanagementPanel(adminId);
            LoadFormInPanel(tenantmanagementPanel);

            //transition
            managetenant_Btn.Image = Properties.Resources.admin_manage_tenant_pink_butt;
            //plain 
            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt__1_;
            payments_Btn.Image = Properties.Resources.admin_tenant_payment_plain_butt;
            maintenance_Btn.Image = Properties.Resources.admin_maintenance_plain_butt;
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_plain_butt;

            //notif out 
            notifPanel.Visible = false;
        }

        private void manageHouse_Btn_Click(object sender, EventArgs e)
        {
            //to form
            boardining_houses boardining_Houses = new boardining_houses();
            LoadFormInPanel(boardining_Houses);

            //transition
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_pink_butt;

            //plain 
            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            managetenant_Btn.Image = Properties.Resources.admin_manage_tenant_plain_butt;
            payments_Btn.Image = Properties.Resources.admin_tenant_payment_plain_butt;
            maintenance_Btn.Image = Properties.Resources.admin_maintenance_plain_butt;
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_plain_butt;

            //notif out 
            notifPanel.Visible = false;
        }

        private void payments_Btn_Click(object sender, EventArgs e)
        {
            //to form
            payment_admin payment_Admin = new payment_admin();
            LoadFormInPanel(payment_Admin);
            //transition
            payments_Btn.Image = Properties.Resources.admin_tenant_payment_pink_butt;

            //plain 
            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            managetenant_Btn.Image = Properties.Resources.admin_manage_tenant_plain_butt;
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt__1_;
            maintenance_Btn.Image = Properties.Resources.admin_maintenance_plain_butt;
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_plain_butt;

            //notif out 
            notifPanel.Visible = false;
        }

        private void maintenance_Btn_Click(object sender, EventArgs e)
        {
            //to form
            admin_maintenance admin_Maintenance = new admin_maintenance();
            LoadFormInPanel(admin_Maintenance);
            //transition
            maintenance_Btn.Image = Properties.Resources.admin_maintenance_pink_butt;

            //plain 
            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            managetenant_Btn.Image = Properties.Resources.admin_manage_tenant_plain_butt;
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt__1_;
            payments_Btn.Image = Properties.Resources.admin_tenant_payment_plain_butt;
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_plain_butt;

            //notif out 
            notifPanel.Visible = false;
        }

        private void analytics_Btn_Click(object sender, EventArgs e)
        {
            //to form
            report_analytics_panel report_Analytics_Panel = new report_analytics_panel();
            LoadFormInPanel(report_Analytics_Panel);

            //transition
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_pink_butt;

            //plain 
            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            managetenant_Btn.Image = Properties.Resources.admin_manage_tenant_plain_butt;
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt__1_;
            payments_Btn.Image = Properties.Resources.admin_tenant_payment_plain_butt;
            maintenance_Btn.Image = Properties.Resources.admin_maintenance_plain_butt;



            //notif out 
            notifPanel.Visible = false;
        }

        private void notification_Btn_Click(object sender, EventArgs e)
        {
            notification_Btn.Image = Properties.Resources.bell;
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
                admin_notif_id, 
                CONCAT(notif_type, ' | ', description, ' | ', DATE_FORMAT(notif_date, '%Y-%m-%d %h:%i %p')) AS Notification, 
                is_read
            FROM admin_notif           
            ORDER BY notif_date DESC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);                  

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable notificationsTable = new DataTable();
                    adapter.Fill(notificationsTable);

                    // Hide `notif_id` and `is_read` columns
                    notificationsTable.Columns.Remove("admin_notif_id");

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

        private void MarkNotificationsAsRead()
        {
            string updateQuery = @"
            UPDATE admin_notif 
            SET is_read = 1  -- Mark notifications as read
            WHERE is_read = 0";  // Only update unread notifications (is_read = 0)

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);             
                cmd.ExecuteNonQuery();
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

        private void profile_picture_Click(object sender, EventArgs e)
        {
            admin_account_profile admin_Account_Profile = new admin_account_profile(verificationCode,adminId);
            LoadFormInPanel(admin_Account_Profile);
        }

        private void clearNotif_Btn_Click(object sender, EventArgs e)
        {
            // Create a new database connection
            string connectionString = dbConnection.GetConnectionString(); // Replace with your actual connection string

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query to delete all records from the admin_notif table
                    string query = "DELETE FROM admin_notif";

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
