using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class tenantcomform : Form
    {
        private int currentTenantId; // Store the current tenant's ID
        private string connectionString; // Connection string to the database

        // Hardcoded admin details
        private const int AdminId = 1; // Hardcoded admin ID
        private const string AdminUsername = "JohnnySins"; // Hardcoded admin username

        private Timer messageRefreshTimer; // Declare a timer

        public tenantcomform(int tenantId) // Pass tenant ID to the constructor
        {
            InitializeComponent();
            currentTenantId = tenantId;

            DatabaseConnection db = new DatabaseConnection();
            connectionString = db.GetConnectionString();

            LoadAdminUsernames(); // Load the single admin into the combo box
            LoadMessages(); // Load messages on form initialization
            InitializeTimer(); // Initialize the timer
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
            LoadMessages(); // Refresh messages on timer tick
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

            using (MySqlConnection connection = new MySqlConnection(connectionString))
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
            using (MySqlConnection connection = new MySqlConnection(connectionString))
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

        private void adminlistCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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

        }
    }
}
