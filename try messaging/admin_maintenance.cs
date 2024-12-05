using System;
using System.Data;
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

        // The admin can choose in a drop-down box the status of maintenance request: Done, In Progress, Pending
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
            typeCombo.Items.AddRange(new string[] { "Electrical", "Plumbing", "HVAC", "Furniture", "Appliances", "Structural", "Pest Control", "Internet/Networking", "Cleaning", "Security", "General Repairs" });
            statusBox1.Items.AddRange(new string[] { "Done", "In Progress", "Pending" });

            // Set default values for date pickers
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1); // Default to 1 month ago
            dateTimePicker1.Value = DateTime.Now; // Default to today

            // Configure DataGridView
            maintenanceRequestList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            maintenanceRequestList.MultiSelect = false;

            // Load all requests
            LoadMaintenanceRequests();
        }

        private void typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Trigger search when the type filter changes
            PerformSearch();
        }

        private void LoadMaintenanceRequests(string searchKeyword = "", string maintenanceType = "", string status = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
            string query = @"SELECT 
                                mr.request_id, 
                                CONCAT(td.lastname, ', ', td.firstname) AS tenant_name, 
                                td.roomnumber, 
                                mr.maintenance_type, 
                                mr.description, 
                                mr.request_date, 
                                mr.status
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
                    dataTable.Columns["tenant_name"].ColumnName = "Tenant Name";
                    dataTable.Columns["roomnumber"].ColumnName = "Room Number";
                    dataTable.Columns["maintenance_type"].ColumnName = "Maintenance Type";
                    dataTable.Columns["description"].ColumnName = "Description";
                    dataTable.Columns["request_date"].ColumnName = "Request Date";
                    dataTable.Columns["status"].ColumnName = "Status";

                    // Bind the data to the DataGridView
                    maintenanceRequestList.DataSource = dataTable;

                    // Set the columns to fill the DataGridView
                    maintenanceRequestList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Update summary statistics
                    UpdateSummaryStatistics(dataTable);

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

        private void UpdateSummaryStatistics(DataTable dataTable)
        {
            // Update total requests
            totalRequests.Text = dataTable.Rows.Count.ToString();

            // Update unopened requests
            unopenedRequests.Text = dataTable.AsEnumerable().Count(row => row["Status"].ToString() == "Pending").ToString();

            // Update resolved requests
            resolvedRequests.Text = dataTable.AsEnumerable().Count(row => row["Status"].ToString() == "Done").ToString();
        }

        private void PerformSearch()
        {
            string searchKeyword = searchBar.Text.Trim(); // Trim whitespace
            string maintenanceType = typeCombo.SelectedItem?.ToString(); // Get selected maintenance type
            string status = statusBox1.SelectedItem?.ToString(); // Get selected status
            DateTime? startDate = dateTimePicker1.Value.Date; // Use date only
            DateTime? endDate = dateTimePicker1.Value.Date; // Use date only

            // Debugging output
            MessageBox.Show($"Searching for type: {maintenanceType}"); // Debugging line

            // Load maintenance requests with the selected filters
            LoadMaintenanceRequests(searchKeyword, maintenanceType, status, startDate, endDate);
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

                    // Retrieve the Request ID from the selected row
                    int requestId = Convert.ToInt32(selectedRow.Cells["Request ID"].Value);

                    // Open the MaintenanceRequestForm with the selected Request ID
                    MaintenanceRequestForm requestForm = new MaintenanceRequestForm(requestId, this);
                    requestForm.ShowDialog(); // Show the form as a dialog
                }
                else
                {
                    MessageBox.Show("Please select a maintenance request from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while opening the maintenance request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handlers for text change events (if needed)
        private void resolvedRequests_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event for resolvedRequests
        }

        private void unopenedRequests_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event for unopenedRequests
        }

        private void totalRequests_TextChanged(object sender, EventArgs e)
        {
            // Handle text changed event for totalRequests
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }
    }
}
