using MySql.Data.MySqlClient; // Ensure this namespace is included
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class admin_dashboard : Form
    {
        private Timer messageCheckTimer; // Timer to check for new messages
        private DatabaseConnection dbConnection;
        private int adminId;
        private Timer clockTimer;
        private string verificationCode; // To store the verification code

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
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt;
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
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt;
            payments_Btn.Image = Properties.Resources.admin_tenant_payment_plain_butt;
            maintenance_Btn.Image = Properties.Resources.admin_maintenance_plain_butt;
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_plain_butt;
            //notif out 
            notificationGrid.Visible = false;
        }

        private void managetenant_Btn_Click(object sender, EventArgs e)
        {
            tenantmanagementPanel tenantmanagementPanel = new tenantmanagementPanel(adminId);
            LoadFormInPanel(tenantmanagementPanel);

            //transition
            managetenant_Btn.Image = Properties.Resources.admin_manage_tenant_pink_butt;
            //plain 
            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt;
            payments_Btn.Image = Properties.Resources.admin_tenant_payment_plain_butt;
            maintenance_Btn.Image = Properties.Resources.admin_maintenance_plain_butt;
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_plain_butt;

            //notif out 
            notificationGrid.Visible = false;
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
            notificationGrid.Visible = false;
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
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt;
            maintenance_Btn.Image = Properties.Resources.admin_maintenance_plain_butt;
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_plain_butt;

            //notif out 
            notificationGrid.Visible = false;
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
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt;
            payments_Btn.Image = Properties.Resources.admin_tenant_payment_plain_butt;
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_plain_butt;

            //notif out 
            notificationGrid.Visible = false;
        }

        private void analytics_Btn_Click(object sender, EventArgs e)
        {
            //to form

            //transition
            analytics_Btn.Image = Properties.Resources.admin_report_analytic_pink_butt;

            //plain 
            dashboard_Btn.Image = Properties.Resources.dashboard_plain_butt__2_;
            managetenant_Btn.Image = Properties.Resources.admin_manage_tenant_plain_butt;
            manageHouse_Btn.Image = Properties.Resources.admin_manage_house_plain_butt;
            payments_Btn.Image = Properties.Resources.admin_tenant_payment_plain_butt;
            maintenance_Btn.Image = Properties.Resources.admin_maintenance_plain_butt;
            


            //notif out 
            notificationGrid.Visible = false;
        }

        private void notification_Btn_Click(object sender, EventArgs e)
        {
            notificationGrid.Visible = !notificationGrid.Visible;
        }

        private void profile_picture_Click(object sender, EventArgs e)
        {
            admin_account_profile admin_Account_Profile = new admin_account_profile(verificationCode,adminId);
            LoadFormInPanel(admin_Account_Profile);
        }
    }
}
