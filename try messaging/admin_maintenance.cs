using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class admin_maintenance : Form
    {
        public admin_maintenance()
        {
            InitializeComponent();
            LoadMaintenanceRequests(); // Load all requests initially
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        // The admin can type to search here
        private void searchBar_TextChanged(object sender, EventArgs e)
        {
            // Trigger search as the admin types
            PerformSearch();
        }

        // This is the search button
        private void searchButton_Click(object sender, EventArgs e)
        {
            // Trigger search when the admin clicks the button
            PerformSearch();
        }

        // The admin can choose in a drop-down box the status of maintenance request: Done, In Progress, Pending, Declined
        private void statusBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Trigger search when the status filter changes
            PerformSearch();
        }

        // The admin can choose the date range of the maintenance request they are looking for
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Trigger search when the start date changes
            PerformSearch();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // Trigger search when the end date changes
            PerformSearch();
        }

        private void maintenanceRequestList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void admin_maintenance_Load(object sender, EventArgs e)
        {
            // Populate dropdowns with predefined types and statuses
            typeCombo.Items.AddRange(new string[]
            {
                "Electrical",
                "Plumbing",
                "HVAC (Heating, Ventilation, and Air Conditioning)", // Updated entry
                "Furniture",
                "Appliances",
                "Structural",
                "Pest Control",
                "Internet/Networking",
                "Cleaning",
                "Security",
                "General Repairs"
            });

            statusCombo.Items.AddRange(new string[] { "Done", "In Progress", "Pending", "Declined" });

            // Set default values for date pickers
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1); // Default to 1 month ago
            dateTimePicker1.Value = DateTime.Now; // Default to today

            // Configure DataGridView
            maintenanceRequestList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            maintenanceRequestList.MultiSelect = false;

            // Load all requests
            LoadMaintenanceRequests();
            UpdateSummaryStatistics();
        }


        private void typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected maintenance type
            string selectedType = typeCombo.SelectedItem?.ToString();

            // Call the method to load maintenance requests with the selected type as a filter
            LoadMaintenanceRequests(searchKeyword: searchBar.Text.Trim(), maintenanceType: selectedType);
        }

        private void LoadMaintenanceRequests(string searchKeyword = "", string maintenanceType = "", string status = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
            string query = @"SELECT 
                mr.request_id, 
                mr.request_number,
                CONCAT(td.lastname, ', ', td.firstname) AS tenant_name, 
                td.roomnumber, 
                mr.maintenance_type, 
                mr.description, 
                mr.request_date, 
                mr.dateInspection, 
                COALESCE(mr.status, 'Pending') AS status  -- Set default status to 'Pending' if null
             FROM 
                maintenance_requests mr
             INNER JOIN 
                tenants_details td ON mr.tenant_id = td.tenid
             WHERE 1=1";

            // Add filters to the query based on input
            if (!string.IsNullOrEmpty(searchKeyword))
                query += " AND (mr.description LIKE @searchKeyword OR mr.maintenance_type LIKE @searchKeyword OR CONCAT(td.lastname, ', ', td.firstname) LIKE @searchKeyword)";

            if (!string.IsNullOrEmpty(maintenanceType))
                query += " AND mr.maintenance_type = @maintenanceType";

            if (!string.IsNullOrEmpty(status))
                query += " AND mr.status = @status";

            if (startDate.HasValue)
                query += " AND mr.request_date >= @startDate";

            if (endDate.HasValue)
                query += " AND mr.request_date <= @endDate";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                // Add parameters for the filters
                if (!string.IsNullOrEmpty(searchKeyword))
                    command.Parameters.AddWithValue("@searchKeyword", $"%{searchKeyword}%");
                if (!string.IsNullOrEmpty(maintenanceType))
                    command.Parameters.AddWithValue("@maintenanceType", maintenanceType);
                if (!string.IsNullOrEmpty(status))
                    command.Parameters.AddWithValue("@status", status);
                if (startDate.HasValue)
                    command.Parameters.AddWithValue("@startDate", startDate.Value);
                if (endDate.HasValue)
                    command.Parameters.AddWithValue("@endDate", endDate.Value);

                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Rename columns for better display
                    dataTable.Columns["request_id"].ColumnName = "Request ID";
                    dataTable.Columns["request_number"].ColumnName = "Request Number";
                    dataTable.Columns["tenant_name"].ColumnName = "Tenant Name";
                    dataTable.Columns["roomnumber"].ColumnName = "Room Number";
                    dataTable.Columns["maintenance_type"].ColumnName = "Maintenance Type";
                    dataTable.Columns["description"].ColumnName = "Description";
                    dataTable.Columns["request_date"].ColumnName = "Request Date";
                    dataTable.Columns["dateInspection"].ColumnName = "Date of Inspection";
                    dataTable.Columns["status"].ColumnName = "Status";

                    // Modify dateInspection values based on status
                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["Status"].ToString() == "Declined")
                        {
                            row["Date of Inspection"] = DBNull.Value; // Set to DBNull for declined requests
                        }
                    }

                    // Bind the data to the DataGridView
                    maintenanceRequestList.DataSource = dataTable;

                    // Set the columns to fill the DataGridView
                    maintenanceRequestList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Show a message if no requests found
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No maintenance requests found with the given filter.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading maintenance requests: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateSummaryStatistics()
        {
            string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
            string query = @"SELECT 
                COUNT(*) AS totalRequests,
                SUM(CASE WHEN status = 'Pending' THEN 1 ELSE 0 END) AS unopenedRequests,
                SUM(CASE WHEN status = 'Done' THEN 1 ELSE 0 END) AS resolvedRequests
             FROM maintenance_requests";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            totalRequests.Text = reader["totalRequests"].ToString();
                            unopenedRequests.Text = reader["unopenedRequests"].ToString();
                            resolvedRequests.Text = reader["resolvedRequests"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating summary statistics: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PerformSearch()
        {
            string searchKeyword = searchBar.Text.Trim(); // Trim whitespace
            string maintenanceType = typeCombo.SelectedItem?.ToString(); // Get selected maintenance type
            string status = statusCombo.SelectedItem?.ToString(); // Get selected status
            DateTime? startDate = dateTimePicker1.Value.Date; // Use date only
            

            string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
            string query = @"SELECT 
                        mr.request_id, 
                        CONCAT(td.lastname, ', ', td.firstname) AS tenant_name, 
                        td.roomnumber, 
                        mr.maintenance_type, 
                        mr.description, 
                        mr.request_date, 
                        mr.dateInspection, 
                        mr.status
                    FROM 
                        maintenance_requests mr
                    INNER JOIN 
                        tenants_details td ON mr.tenant_id = td.tenid
                    WHERE 1=1"; // Base query

            // Add filters to the query based on input
            if (!string.IsNullOrEmpty(searchKeyword))
                query += @" AND (
                mr.request_id LIKE @searchKeyword 
                OR mr.maintenance_type LIKE @searchKeyword 
                OR mr.description LIKE @searchKeyword 
                OR mr.status LIKE @searchKeyword 
                OR mr.dateInspection LIKE @searchKeyword 
                OR CONCAT(td.lastname, ', ', td.firstname) LIKE @searchKeyword 
                OR td.roomnumber LIKE @searchKeyword)";


            if (!string.IsNullOrEmpty(maintenanceType))
                query += " AND mr.maintenance_type = @maintenanceType";

            if (!string.IsNullOrEmpty(status))
                query += " AND mr.status = @status";

            if (startDate.HasValue)
                query += " AND mr.request_date >= @startDate";


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                // Add parameters for the filters
                if (!string.IsNullOrEmpty(searchKeyword))
                    command.Parameters.AddWithValue("@searchKeyword", $"%{searchKeyword}%");
                if (!string.IsNullOrEmpty(maintenanceType))
                    command.Parameters.AddWithValue("@maintenanceType", maintenanceType);
                if (!string.IsNullOrEmpty(status))
                    command.Parameters.AddWithValue("@status", status);
                if (startDate.HasValue)
                    command.Parameters.AddWithValue("@startDate", startDate.Value);
                

                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Rename columns for better display
                    dataTable.Columns["request_id"].ColumnName = "Request ID";
                    dataTable.Columns["tenant_name"].ColumnName = "Tenant Name";
                    dataTable.Columns["roomnumber"].ColumnName = "Room Number";
                    dataTable.Columns["maintenance_type"].ColumnName = "Maintenance Type";
                    dataTable.Columns["description"].ColumnName = "Description";
                    dataTable.Columns["request_date"].ColumnName = "Request Date";
                    dataTable.Columns["dateInspection"].ColumnName = "Date of Inspection";
                    dataTable.Columns["status"].ColumnName = "Status";

                    // Bind the data to the DataGridView
                    maintenanceRequestList.DataSource = dataTable;

                    // Set the columns to fill the DataGridView
                    maintenanceRequestList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Show a message if no requests found
                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No maintenance requests found with the given filter.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching maintenance requests: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void HighlightMatches(string searchKeyword)
        {
            if (string.IsNullOrEmpty(searchKeyword))
            {
                // If no keyword, reset highlighting
                foreach (DataGridViewRow row in maintenanceRequestList.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.BackColor = Color.White; // Reset to default color
                    }
                }
                return;
            }

            foreach (DataGridViewRow row in maintenanceRequestList.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().IndexOf(searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        cell.Style.BackColor = Color.Yellow; // Highlight matching cell
                    }
                    else
                    {
                        cell.Style.BackColor = Color.White; // Reset to default color if not matching
                    }
                }
            }
        }

        public void RefreshDataGridView()
        {
            LoadMaintenanceRequests(); // Reload the data
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure a row is selected
                if (maintenanceRequestList.SelectedRows.Count > 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = maintenanceRequestList.SelectedRows[0];

                    // Get the Request ID of the selected row
                    int requestId = Convert.ToInt32(selectedRow.Cells["Request ID"].Value);

                    // Open the MaintenanceRequestForm and pass the request ID
                    MaintenanceRequestForm requestForm = new MaintenanceRequestForm(requestId, this); // Pass reference to admin_maintenance
                    requestForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select a maintenance request to open.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening the maintenance request form: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to update the inspection date
        public void UpdateInspectionDate(int requestId, DateTime inspectionDate)
        {
            string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
            string query = "UPDATE maintenance_requests SET dateInspection = @inspectionDate WHERE request_id = @requestId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@inspectionDate", inspectionDate);
                command.Parameters.AddWithValue("@requestId", requestId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating inspection date: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Handle text changed event for resolvedRequests
        private void resolvedRequests_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event for resolvedRequests
        }

        // Handle text changed event for unopenedRequests
        private void unopenedRequests_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event for unopenedRequests
        }

        // Handle text changed event for totalRequests
        private void totalRequests_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event for totalRequests
        }

        // Event handler for label click
        private void label13_Click(object sender, EventArgs e)
        {
        }

        private void searchBar_TextChanged_1(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void statusCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected status
            string selectedStatus = statusCombo.SelectedItem?.ToString();

            // Call the method to load maintenance requests with the selected status as a filter
            LoadMaintenanceRequests(searchKeyword: searchBar.Text.Trim(), status: selectedStatus);
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            // Get the selected date from the DateTimePicker
            DateTime selectedDate = dateTimePicker1.Value.Date;

            // Call the method to load maintenance requests with the selected date as a filter
            LoadMaintenanceRequests(searchKeyword: searchBar.Text.Trim(), startDate: selectedDate);
        }
    }
}
