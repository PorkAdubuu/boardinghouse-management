using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class admin_dashboard_display : Form
    {
        private DatabaseConnection dbConnection;

        public admin_dashboard_display()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();

            // Set placeholder text color on initialization
            annoucementText.Text = "Enter announcement here...";
            annoucementText.ForeColor = Color.Gray;
        }

        private void annoucementText_Enter(object sender, EventArgs e)
        {
            if (annoucementText.Text == "Enter announcement here...")
            {
                annoucementText.Text = "";
                annoucementText.ForeColor = Color.Black;
            }
        }

        private void annoucementText_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(annoucementText.Text))
            {
                annoucementText.Text = "Enter announcement here...";
                annoucementText.ForeColor = Color.Gray;
            }
        }

        private void announcement_Btn_Click(object sender, EventArgs e)
        {
            string announceText = annoucementText.Text.Trim();

            // Check if the announcement text is valid
            if (string.IsNullOrEmpty(announceText) || announceText == "Enter announcement here...")
            {
                MessageBox.Show("Please enter a valid announcement.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insert the announcement into the database
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO announcements (message) VALUES (@message)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@message", announceText);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Announcement successfully posted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAnnouncements();
                    // Clear the text box and reset placeholder
                    annoucementText.Text = "Enter announcement here...";
                    annoucementText.ForeColor = Color.Gray;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error making announcement: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadAnnouncements()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT message, date_time FROM announcements ORDER BY date_time DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        announcementLog.Clear(); // Clear existing content

                        while (reader.Read())
                        {
                            string dateTime = Convert.ToDateTime(reader["date_time"]).ToString("yyyy-MM-dd HH:mm:ss");
                            string message = reader["message"].ToString();

                            // Automated announcement format
                            string formattedAnnouncement = $"[{dateTime}]\nGood day, everyone!\n\nThis is your admin, and I would like to share an important announcement:\n\n{message}\n\nThank you for your attention!\n\n";

                            // Append formatted announcement with color
                            announcementLog.SelectionStart = announcementLog.TextLength;
                            announcementLog.SelectionColor = Color.Blue; // Date-Time Color
                            announcementLog.AppendText($"[{dateTime}]\n");

                            announcementLog.SelectionStart = announcementLog.TextLength;
                            announcementLog.SelectionColor = Color.Black; // Message Color
                            announcementLog.AppendText("Good day, everyone!\n\nThis is your admin, and I would like to share an important announcement:\n\n");

                            announcementLog.SelectionStart = announcementLog.TextLength;
                            announcementLog.SelectionColor = Color.Green; // Highlight Message
                            announcementLog.AppendText($"{message}\n\n");

                            announcementLog.SelectionStart = announcementLog.TextLength;
                            announcementLog.SelectionColor = Color.Gray; // Closure Color
                            announcementLog.AppendText("Thank you for your attention!\n\n");

                            // Reset color
                            announcementLog.SelectionColor = announcementLog.ForeColor;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading announcements: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void admin_dashboard_display_Load(object sender, EventArgs e)
        {
            LoadAnnouncements();
        }
    }
}
