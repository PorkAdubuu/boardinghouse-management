﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Data.Common;
using System.IO;
using System.Data.Odbc;

namespace try_messaging
{
    public partial class admincomform : Form
    {
        private string connectionString = "Server=localhost;Database=boardinghouse_practice_db;Uid=root;Pwd=;"; // Update with your database connection string
        private Timer messageRefreshTimer; // Declare a timer for conversationBox
        private Timer notificationRefreshTimer; // Timer for notification updates
        private int? selectedTenantId = null; 


        public admincomform()
        {
            InitializeComponent();
            InitializeTimer(); 
            this.CenterToScreen();
            this.BackColor = Color.WhiteSmoke;
            LoadTenantList();
        }

        private void InitializeTimer()
        {
            //for messages
            messageRefreshTimer = new Timer();
            messageRefreshTimer.Interval = 3000; // Set the timer 3 secs
            messageRefreshTimer.Tick += MessageRefreshTimer_Tick; // Attach the tick event handler
            // for notificaiton
            notificationRefreshTimer = new Timer();
            notificationRefreshTimer.Interval = 3000; // 3 secs
            notificationRefreshTimer.Tick += NotificationRefreshTimer_Tick;
            notificationRefreshTimer.Start(); // Start the timer 
        }

        private void admincomform_Load(object sender, EventArgs e)
        {
            LoadTenantList();

            if (tenantlistsGrid.Rows.Count > 0)
            {
                tenantlistsGrid.Rows[0].Selected = true;
                tenantlistsGrid_CellContentClick(this, new DataGridViewCellEventArgs(0, 0));
            }
        }


        // Method to load tenant names and room numbers into the DataGridView
        private void LoadTenantList()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Modified query to include a check for unread messages
                    MySqlCommand cmd = new MySqlCommand(
                        "SELECT CONCAT(firstname, ' ', lastname, ' (Room ', roomnumber, ')') AS DisplayText, tenid, " +
                        "(SELECT COUNT(*) FROM combined_messages WHERE sender_id = tenants_details.tenid AND sender_type = 'tenant' AND is_read = 0) AS UnreadCount " +
                        "FROM tenants_details", conn);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable tenantTable = new DataTable();
                    adapter.Fill(tenantTable);

                    tenantlistsGrid.DataSource = tenantTable;
                    tenantlistsGrid.Columns["tenid"].Visible = false; // Hide the tenant ID column

                    tenantlistsGrid.Columns["DisplayText"].HeaderText = "                            Tenants\n                       (Name & Room)";
                    tenantlistsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Adjust the column width

                    tenantlistsGrid.ClearSelection();

                    // Check for unread messages and apply color formatting
                    foreach (DataGridViewRow row in tenantlistsGrid.Rows)
                    {
                        int unreadCount = Convert.ToInt32(row.Cells["UnreadCount"].Value);

                        if (unreadCount > 0)
                        {
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow; // Highlight color for unread messages
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.White; // Default color
                        }
                    }

                    tenantlistsGrid.Columns["UnreadCount"].Visible = false; // Hide the unread count column after processing
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tenants: " + ex.Message);
                }
            }
        }
        private void NotificationRefreshTimer_Tick(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query to get tenant IDs with unread messages
                    MySqlCommand cmd = new MySqlCommand(
                        "SELECT DISTINCT sender_id FROM combined_messages WHERE sender_type = 'tenant' AND is_read = 0", conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Reset all rows to default color first
                    foreach (DataGridViewRow row in tenantlistsGrid.Rows)
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }

                    // Highlight rows with unread messages
                    while (reader.Read())
                    {
                        int unreadTenantId = reader.GetInt32("sender_id");

                        foreach (DataGridViewRow row in tenantlistsGrid.Rows)
                        {
                            if (Convert.ToInt32(row.Cells["tenid"].Value) == unreadTenantId)
                            {
                                row.DefaultCellStyle.BackColor = Color.LightYellow; // Set color for unread messages
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking unread messages: " + ex.Message);
                }
            }
        }

        private void MessageRefreshTimer_Tick(object sender, EventArgs e)
        {
            if (tenantlistsGrid.SelectedRows.Count > 0)
            {
                int tenantId = Convert.ToInt32(tenantlistsGrid.SelectedRows[0].Cells["tenid"].Value);
                LoadConversation(tenantId); // Refresh the conversation for the selected tenant
            }
        }

        // Load the conversation for the selected tenant
        private void LoadConversation(int tenantId)
        {
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
            // Scroll to the bottom of the conversationBox
            conversationBox.SelectionStart = conversationBox.Text.Length;
            conversationBox.ScrollToCaret();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            if (selectedTenantId.HasValue) // Check if a tenant has been selected
            {
                int tenantId = selectedTenantId.Value;
                string messageText = typeMessage.Text.Trim();

                if (!string.IsNullOrEmpty(messageText))
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            // Get the logged-in admin's ID from GlobalSettings
                            int adminId = GlobalSettings.LoggedInAdminId;  // Use the dynamic admin ID

                            // Insert into admin_messages table
                            MySqlCommand cmd = new MySqlCommand("INSERT INTO admin_messages (admin_id, message) VALUES (@adminId, @message)", conn);
                            cmd.Parameters.AddWithValue("@adminId", adminId); // Use the dynamic admin ID
                            cmd.Parameters.AddWithValue("@message", messageText);
                            cmd.ExecuteNonQuery();

                            // Insert into combined_messages table
                            cmd = new MySqlCommand("INSERT INTO combined_messages (sender_id, sender_type, recipient_id, message) VALUES (@senderId, 'admin', @recipientId, @message)", conn);
                            cmd.Parameters.AddWithValue("@senderId", adminId); // Use the dynamic admin ID
                            cmd.Parameters.AddWithValue("@recipientId", tenantId); // Use the selected tenant ID
                            cmd.Parameters.AddWithValue("@message", messageText);
                            cmd.ExecuteNonQuery();

                            typeMessage.Clear();
                            LoadConversation(tenantId); // Refresh the conversation box
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

        private void tenantlistsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is selected
            {
                DataGridViewRow row = tenantlistsGrid.Rows[e.RowIndex];
                selectedTenantId = Convert.ToInt32(row.Cells["tenid"].Value); // Store tenant ID

                // Mark messages as read in the database for the selected tenant
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Query to fetch tenant's name and profile picture
                        MySqlCommand cmd = new MySqlCommand(
                            "SELECT CONCAT(firstname, ' ', lastname) AS FullName, profile_picture " +
                            "FROM tenants_details WHERE tenid = @tenantId", conn);
                        cmd.Parameters.AddWithValue("@tenantId", selectedTenantId);

                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            // Update tenantName label
                            tenantName.Text = reader["FullName"].ToString();

                            // Update tenantProfile picture box
                            if (reader["profile_picture"] != DBNull.Value)
                            {
                                byte[] imageBytes = (byte[])reader["profile_picture"];
                                using (var ms = new System.IO.MemoryStream(imageBytes))
                                {
                                    tenantProfile.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                // Set a default image if no profile picture exists
                                tenantProfile.Image = Properties.Resources.DefaultProfile;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading tenant details: " + ex.Message);
                    }
                }

                // Change the background color of the selected row back to default after marking as read
                row.DefaultCellStyle.BackColor = System.Drawing.Color.White;

                // Load the conversation for the selected tenant
                LoadConversation((int)selectedTenantId);
                messageRefreshTimer.Start(); // Start the timer when a tenant is selected
            }
        }

        private void search_Btn_Click(object sender, EventArgs e)
        {
            // Get the search term from the searchBar textbox
            string searchTerm = searchBar.Text.Trim();

            // Only proceed if the search term is not empty
            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Call a method to search the tenants in the database based on the search term
                SearchTenants(searchTerm);
            }
            else
            {
                MessageBox.Show("Please enter a search term.");
            }
        }
        private void SearchTenants(string searchTerm)
        {
            // Create your SQL query to search the tenants and format the "name & room" in the DisplayText column
            string query = "SELECT CONCAT(firstname, ' ', lastname, ' (Room ', roomnumber, ')') AS DisplayText, tenid " +
                           "FROM tenants_details WHERE lastname LIKE @searchTerm OR firstname LIKE @searchTerm OR roomnumber LIKE @searchTerm";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    // Execute the query and store the result
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Display the search results in the DataGridView
                    tenantlistsGrid.DataSource = dt;

                    // Set the column header to "Tenants (Name & Room)"
                    tenantlistsGrid.Columns["DisplayText"].HeaderText = "                            Tenants\n                       (Name & Room)";

                    // Hide the tenid column as we don't need it for display
                    tenantlistsGrid.Columns["tenid"].Visible = false;

                    // Adjust the column width to fit the data
                    tenantlistsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Optionally, select the first row after the search results are displayed
                    if (tenantlistsGrid.Rows.Count > 0)
                    {
                        tenantlistsGrid.Rows[0].Selected = true;
                        tenantlistsGrid_CellContentClick(this, new DataGridViewCellEventArgs(0, 0)); // Trigger the click event for the first row (optional)
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        




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
