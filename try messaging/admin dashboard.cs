using MySql.Data.MySqlClient; // Ensure this namespace is included
using System;
using System.Drawing;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class admin_dashboard : Form
    {
        private Timer messageCheckTimer; // Timer to check for new messages
        private DatabaseConnection dbConnection; 

        public admin_dashboard()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.BackColor = ColorTranslator.FromHtml("#f7f7f7");
            dbConnection = new DatabaseConnection(); // Initialize the DatabaseConnection
            InitializeMessageCheckTimer(); // Initialize the timer
            CheckForNewMessages();
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

        private void admin_dashboard_Load(object sender, EventArgs e)
        {
            // Initial load actions can be performed here if needed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tenantmanagement tenantmanagement = new tenantmanagement();
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
            
        }
    }
}
