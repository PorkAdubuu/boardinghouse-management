using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class admincomform : Form
    {
        private string connectionString = "Server=localhost;Database=boardinghouse_practice_db;Uid=root;Pwd=;"; // Update with your database connection string
        private Timer messageRefreshTimer; // Declare a timer

        public admincomform()
        {
            InitializeComponent();
            InitializeTimer(); // Initialize the timer
        }

        private void InitializeTimer()
        {
            messageRefreshTimer = new Timer();
            messageRefreshTimer.Interval = 3000; // Set the timer interval to 5 seconds (5000 milliseconds)
            messageRefreshTimer.Tick += MessageRefreshTimer_Tick; // Attach the tick event handler
        }

        private void admincomform_Load(object sender, EventArgs e)
        {
            LoadTenantList();
        }

        // Method to load tenant names into the ComboBox
        private void LoadTenantList()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "SELECT CONCAT(firstname, ' ', lastname) AS FullName, roomnumber, tenid FROM tenants_details", conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    tenantlistCombo.Items.Clear();
                    while (reader.Read())
                    {
                        // Format display to include full name and room number
                        string displayText = $"{reader["FullName"]} (Room {reader["roomnumber"]})";
                        tenantlistCombo.Items.Add(new ComboBoxItem(displayText, reader["tenid"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tenants: " + ex.Message);
                }
            }
        }

        private void tenantlistCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the conversationBox when a new tenant is selected
            conversationBox.Clear();
            LoadConversation();
            messageRefreshTimer.Start(); // Start the timer when a tenant is selected
        }

        private void MessageRefreshTimer_Tick(object sender, EventArgs e)
        {
            // Refresh the conversation
            LoadConversation();
        }

        // Load the conversation for the selected tenant
        private void LoadConversation()
        {
            if (tenantlistCombo.SelectedItem is ComboBoxItem selectedTenant)
            {
                int tenantId = int.Parse(selectedTenant.Value);

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Mark messages as read
                        MySqlCommand updateCmd = new MySqlCommand(
                            "UPDATE combined_messages SET is_read = 1 WHERE sender_id = @tenantId AND sender_type = 'tenant' AND is_read = 0", conn);
                        updateCmd.Parameters.AddWithValue("@tenantId", tenantId);
                        updateCmd.ExecuteNonQuery();

                        // Query to select messages along with timestamp
                        MySqlCommand cmd = new MySqlCommand(
                            "SELECT sender_type, message, timestamp FROM combined_messages " +
                            "WHERE (sender_id = @tenantId AND sender_type = 'tenant') OR " +
                            "(recipient_id = @tenantId AND sender_type = 'admin')", conn);
                        cmd.Parameters.AddWithValue("@tenantId", tenantId);

                        MySqlDataReader reader = cmd.ExecuteReader();
                        conversationBox.Clear();

                        while (reader.Read())
                        {
                            string senderType = reader["sender_type"].ToString();
                            string message = reader["message"].ToString();
                            DateTime timestamp = Convert.ToDateTime(reader["timestamp"]);

                            // Set color based on sender type
                            if (senderType == "admin")
                            {
                                conversationBox.SelectionColor = System.Drawing.Color.Blue; // Admin messages in blue
                                conversationBox.AppendText($"You: {message}\n(Sent at: {timestamp})\n\n");
                            }
                            else
                            {
                                conversationBox.SelectionColor = System.Drawing.Color.Green; // Tenant messages in green
                                conversationBox.AppendText($"{senderType}: {message}\n(Sent at: {timestamp})\n\n");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading conversation: " + ex.Message);
                    }
                }
            }
            // Scroll to the bottom of the conversationBox
            conversationBox.SelectionStart = conversationBox.Text.Length;
            conversationBox.ScrollToCaret();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            if (tenantlistCombo.SelectedItem is ComboBoxItem selectedTenant)
            {
                int tenantId = int.Parse(selectedTenant.Value);
                string messageText = typeMessage.Text.Trim();

                if (!string.IsNullOrEmpty(messageText))
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            // Insert into admin_messages table
                            MySqlCommand cmd = new MySqlCommand("INSERT INTO admin_messages (admin_id, message) VALUES (@adminId, @message)", conn);
                            cmd.Parameters.AddWithValue("@adminId", /* Admin ID */ 1); // Replace with actual admin ID
                            cmd.Parameters.AddWithValue("@message", messageText);
                            cmd.ExecuteNonQuery();

                            // Insert into combined_messages table
                            cmd = new MySqlCommand("INSERT INTO combined_messages (sender_id, sender_type, recipient_id, message) VALUES (@senderId, 'admin', @recipientId, @message)", conn);
                            cmd.Parameters.AddWithValue("@senderId", /* Admin ID */ 1); // Replace with actual admin ID
                            cmd.Parameters.AddWithValue("@recipientId", tenantId); // This should be the selected tenant ID
                            cmd.Parameters.AddWithValue("@message", messageText);
                            cmd.ExecuteNonQuery();

                            typeMessage.Clear();
                            LoadConversation(); // Refresh the conversation box
                            conversationBox.SelectionStart = conversationBox.Text.Length;
                            conversationBox.ScrollToCaret();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error sending message: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a message to send.");
                }
            }
            else
            {
                MessageBox.Show("Please select a tenant.");
            }
        }

        private void conversationBox_TextChanged(object sender, EventArgs e)
        {
            // Empty tool code preserved
        }

        private void typeMessage_TextChanged(object sender, EventArgs e)
        {
            // Empty tool code preserved
        }

        private void messagesentlabel_Click(object sender, EventArgs e)
        {
            // Empty tool code preserved
        }
    }

    // Helper class for ComboBox items
    public class ComboBoxItem
    {
        public string Text { get; }
        public string Value { get; }

        public ComboBoxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
