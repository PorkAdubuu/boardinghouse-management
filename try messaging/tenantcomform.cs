using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class tenantcomform : Form
    {
        private int currentTenantId; // Store the current tenant's ID
        private DatabaseConnection dbConnection; // Database connection handler
        private Timer messageRefreshTimer; // Declare a timer

        

        public tenantcomform(int tenantId) // Pass tenant ID to the constructor
        {
            InitializeComponent();
            currentTenantId = tenantId;
            dbConnection = new DatabaseConnection();
            MarkMessagesAsRead();
            LoadMessages();
            InitializeTimer();

            //this.BackColor = ColorTranslator.FromHtml("#f7f7f7");
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

        private void InitializeTimer()
        {
            messageRefreshTimer = new Timer();
            messageRefreshTimer.Interval = 5000; // Set the timer interval to 5 seconds (5000 milliseconds)
            messageRefreshTimer.Tick += MessageRefreshTimer_Tick;
            messageRefreshTimer.Start(); // Start the timer
        }

        private void MessageRefreshTimer_Tick(object sender, EventArgs e)
        {
            if (!this.IsDisposed && conversationBox.IsHandleCreated)
            {
                LoadMessages(); // Refresh messages on timer tick
            }
        }

        private void LoadAdminUsernames()
        {
            
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            string message = typeMessage.Text;

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                string query = "INSERT INTO combined_messages (sender_id, sender_type, message) VALUES (@senderId, @senderType, @message)";
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@senderId", currentTenantId); // Use the current tenant ID
                command.Parameters.AddWithValue("@senderType", "tenant"); // Specify sender type as 'tenant'
                command.Parameters.AddWithValue("@message", message);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    typeMessage.Clear();
                    LoadMessages(); // Load messages after sending
                    conversationBox.SelectionStart = conversationBox.TextLength; // Scroll to the bottom
                    conversationBox.ScrollToCaret(); // Ensure the caret is visible
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error sending message: " + ex.Message);
                }
            }
        }

        private void LoadMessages()
        {
            conversationBox.Clear();
            using (MySqlConnection connection = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                string query = @"
                SELECT sender_type, message, timestamp 
                FROM combined_messages 
                WHERE 
                    (sender_id = @tenantId AND sender_type = 'tenant') OR 
                    (recipient_id = @tenantId AND sender_type = 'admin') 
                ORDER BY timestamp ASC;";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@tenantId", currentTenantId);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string senderType = reader["sender_type"].ToString();
                        string message = reader["message"].ToString();
                        DateTime timestamp = Convert.ToDateTime(reader["timestamp"]);

                        // Set color based on sender type
                        if (senderType == "admin")
                        {
                            conversationBox.SelectionColor = System.Drawing.Color.Blue; // Admin messages in blue
                            conversationBox.AppendText($"admin: {message}\n(Sent at: {timestamp})\n\n");
                        }
                        else
                        {
                            conversationBox.SelectionColor = System.Drawing.Color.Black; // Tenant messages in black
                            conversationBox.AppendText($"You: {message}\n(Sent at: {timestamp})\n\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading messages: " + ex.Message);
                }
            }
            // Scroll to the bottom of the conversationBox
            conversationBox.SelectionStart = conversationBox.Text.Length;
            conversationBox.ScrollToCaret();
        }

        private void tenantcomform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (messageRefreshTimer != null)
            {
                messageRefreshTimer.Stop();
                messageRefreshTimer.Dispose();
            }
        }

        private void adminlistCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle changes in the admin list selection here if needed
        }

        private void conversationBox_TextChanged(object sender, EventArgs e) { }
        private void typeMessage_TextChanged(object sender, EventArgs e) { }
        private void messagesentlabel_Click(object sender, EventArgs e) { }

        // Define a class for Admin items
        private class AdminItem
        {
            public string Username { get; set; }
            public int Id { get; set; }

            public override string ToString()
            {
                return Username;
            }
        }

        private void tenantcomform_Load(object sender, EventArgs e)
        {
            // Code to execute on form load if needed
        }
        private void typeMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                // Disable Enter key
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter && e.Shift)
            {
                // Allow Shift+Enter to insert a new line
                int cursorPosition = typeMessage.SelectionStart;
                typeMessage.Text = typeMessage.Text.Insert(cursorPosition, "\n");
                typeMessage.SelectionStart = cursorPosition + 1;
                e.Handled = true;
            }
        }


    }
}
