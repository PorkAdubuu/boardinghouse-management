using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
        
        private void UpdatePieChart()
        {
            // Create a new database connection
            string connectionString = dbConnection.GetConnectionString(); // Replace with your actual connection string

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query to count paid and unpaid bills
                    string query = @"
                        SELECT 
                            COUNT(*) AS TotalBills,
                            SUM(CASE WHEN status = 'Paid' THEN 1 ELSE 0 END) AS PaidCount,
                            SUM(CASE WHEN status IN ('No payment', 'Pending', 'Declined') THEN 1 ELSE 0 END) AS UnpaidCount
                        FROM billing_table";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Execute query and get result
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int totalBills = reader.GetInt32("TotalBills");
                                int paidCount = reader.GetInt32("PaidCount");
                                int unpaidCount = reader.GetInt32("UnpaidCount");

                                // Check if there are any bills to avoid division by zero
                                if (totalBills == 0)
                                {
                                    MessageBox.Show("No bills found in the database.");
                                    return;
                                }

                                // Calculate the percentages for paid and unpaid bills
                                double paidPercentage = (double)paidCount / totalBills * 1;
                                double unpaidPercentage = (double)unpaidCount / totalBills * 1;

                                // Update the pie chart data
                                chart1.Series.Clear();
                                Series pieSeries = chart1.Series.Add("Payment Status");
                                pieSeries.ChartType = SeriesChartType.Pie;

                                // Add the data points (Paid and Unpaid)
                                pieSeries.Points.Clear();
                                pieSeries.Points.AddXY("Paid", paidPercentage);
                                pieSeries.Points.AddXY("Unpaid", unpaidPercentage);

                                // Optional: Add chart title
                                chart1.Titles.Clear();
                                chart1.Titles.Add("Bill Payment Status");

                                // Optional: Format data labels
                                pieSeries.IsValueShownAsLabel = true;
                                pieSeries.LabelFormat = "#0%"; // Show percentage
                                pieSeries.LabelForeColor = Color.White;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("An error occurred while updating the chart: " + ex.Message);
                }
            }
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
            LoadTotalTenant();
            LoadTotalHouses();
            UpdateBillingCounters();
            UpdateTotalIncome();
            UpdatePieChart();
        }

        private void UpdateBillingCounters()
        {
            string connectionString = dbConnection.GetConnectionString(); // Replace with your actual connection string
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query to get all counts with the new condition for "due" bills
                    string query = @"
                SELECT 
                    COUNT(*) AS total_bills,
                    SUM(CASE WHEN status IN ('No payment', 'Pending', 'Declined') THEN 1 ELSE 0 END) AS no_payment_count,
                    SUM(CASE WHEN DATE(due_date) <= @today AND status != 'Paid' THEN 1 ELSE 0 END) AS due_today_count,
                    SUM(CASE WHEN status = 'Paid' THEN 1 ELSE 0 END) AS paid_count
                FROM billing_table";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@today", today);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the text boxes
                                issuedBillText.Text = reader["total_bills"].ToString();
                                noPaymentText.Text = reader["no_payment_count"].ToString();
                                dueText.Text = reader["due_today_count"].ToString();
                                paidText.Text = reader["paid_count"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while updating billing counters: " + ex.Message);
                }
            }
        }

        private void UpdateTotalIncome()
        {
            string connectionString = dbConnection.GetConnectionString(); // Replace with your actual connection string

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Query to calculate the total income from total_bill in payment_archive_table
                    string query = "SELECT IFNULL(SUM(total_bill), 0) AS total_income FROM payment_archive_table";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Execute the query and get the result
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            // Format the result as currency and display in the textbox
                            totalIncomeText.Text = Convert.ToDecimal(result).ToString("C2");
                        }
                        else
                        {
                            totalIncomeText.Text = "0.00";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while calculating total income: " + ex.Message);
                }
            }
        }



        private void LoadTotalTenant()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    // Query to count the total number of tenants
                    string query = "SELECT COUNT(*) AS TotalTenants FROM tenants_details";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Execute the query and retrieve the total count
                    object result = cmd.ExecuteScalar();
                    int totalTenants = result != null ? Convert.ToInt32(result) : 0;

                    // Display the total number of tenants in the label
                    totalTenantLabel.Text = totalTenants.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading total tenants: " + ex.Message);
                }
            }
        }
        private void LoadTotalHouses()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    // Query to count the total number of tenants
                    string query = "SELECT COUNT(*) AS TotalTenants FROM boarding_houses";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Execute the query and retrieve the total count
                    object result = cmd.ExecuteScalar();
                    int totalTenants = result != null ? Convert.ToInt32(result) : 0;

                    // Display the total number of tenants in the label
                    totalHousesLabel.Text = totalTenants.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading total houses: " + ex.Message);
                }
            }
        }

    }
}
