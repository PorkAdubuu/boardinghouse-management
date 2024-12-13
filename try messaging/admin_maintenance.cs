using System;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class admin_maintenance : Form
    {
        private int selectedRequestId = 0;
        private DatabaseConnection dbConnection;
        public admin_maintenance()
        {
            InitializeComponent();
            LoadMaintenanceRequests(); // Load all requests initially
            dbConnection = new DatabaseConnection();
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
            try
            {
                // Ensure the clicked cell is not a header or outside the valid range
                if (e.RowIndex >= 0 && maintenanceRequestList.Columns[e.ColumnIndex].Name == "Request ID")
                {
                    // Retrieve the value of 'Request ID' from the clicked row
                    selectedRequestId = Convert.ToInt32(maintenanceRequestList.Rows[e.RowIndex].Cells["Request ID"].Value);

                    // Log or show the selected request_id for debugging
                    Console.WriteLine($"Selected Request ID: {selectedRequestId}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting request: " + ex.Message);
            }
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
        mr.completion_date,  -- Include completion_date in the query
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
                    dataTable.Columns["completion_date"].ColumnName = "Completion Date";  // Rename completion_date column
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

        private void markAsDone_Btn_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
            try
            {
                // Ensure a row is selected in the DataGridView
                if (maintenanceRequestList.SelectedRows.Count > 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = maintenanceRequestList.SelectedRows[0];

                    // Get the Request ID of the selected row
                    selectedRequestId = Convert.ToInt32(selectedRow.Cells["Request ID"].Value);

                    // Check if a request ID is selected
                    if (selectedRequestId <= 0)
                    {
                        MessageBox.Show("No request selected. Please select a request first.");
                        return;
                    }

                    if (dbConnection == null)
                    {
                        MessageBox.Show("Database connection is not initialized.");
                        return; // Exit the method if dbConnection is null
                    }

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Check the current status of the request
                        string checkStatusQuery = "SELECT status FROM maintenance_requests WHERE request_id = @requestId";
                        using (MySqlCommand checkCmd = new MySqlCommand(checkStatusQuery, connection))
                        {
                            checkCmd.Parameters.AddWithValue("@requestId", selectedRequestId);
                            string status = checkCmd.ExecuteScalar()?.ToString();

                            // Validate the status
                            if (status == "Done")
                            {
                                MessageBox.Show("This request is already marked as Done.");
                                return;
                            }

                            if (status == "Declined")
                            {
                                MessageBox.Show("This request has been Declined and cannot be marked as Done.");
                                return;
                            }

                            if (status != "In Progress")
                            {
                                MessageBox.Show("Only requests with status 'In Progress' can be marked as Done.");
                                return;
                            }
                        }

                        // Update the status to 'Done' and set the completion date
                        string updateQuery = "UPDATE maintenance_requests SET status = 'Done', completion_date = CURDATE() WHERE request_id = @requestId";
                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@requestId", selectedRequestId);
                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Request successfully marked as Done.");
                            }
                            else
                            {
                                MessageBox.Show("Failed to update the request. Please try again.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No row selected. Please select a maintenance request first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void export_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the current date in MM_dd_yyyy format
                string currentDate = DateTime.Now.ToString("MM_dd_yyyy");

                // Open SaveFileDialog to choose the save location
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Save Excel File";
                saveFileDialog.FileName = $"Maintenance request list_{currentDate}.xlsx"; // Default filename with current date

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Create an instance of Excel application
                    var excelApp = new Microsoft.Office.Interop.Excel.Application();

                    // Check if Excel is available
                    if (excelApp == null)
                    {
                        MessageBox.Show("Excel is not installed on this machine.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Create a new workbook
                    var workbooks = excelApp.Workbooks;
                    var workbook = workbooks.Add();
                    var worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

                    // Loop through DataGridView columns and set the column headers in Excel
                    for (int i = 0; i < maintenanceRequestList.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1] = maintenanceRequestList.Columns[i].HeaderText; // Set headers in Excel (row 1)
                        worksheet.Cells[1, i + 1].Font.Bold = true; // Make headers bold
                    }

                    // Loop through DataGridView rows and add data to Excel
                    for (int rowIndex = 0; rowIndex < maintenanceRequestList.Rows.Count; rowIndex++)
                    {
                        for (int colIndex = 0; colIndex < maintenanceRequestList.Columns.Count; colIndex++)
                        {
                            if (maintenanceRequestList.Rows[rowIndex].Cells[colIndex].Value != null)
                            {
                                worksheet.Cells[rowIndex + 2, colIndex + 1] = maintenanceRequestList.Rows[rowIndex].Cells[colIndex].Value.ToString();
                            }
                        }
                    }

                    // Save the Excel file with the formatted filename
                    workbook.SaveAs(saveFileDialog.FileName);

                    // Close the workbook and Excel application
                    workbook.Close();
                    excelApp.Quit();

                    // Release COM objects
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    // Inform the user that the file has been saved
                    MessageBox.Show("Maintenance request list has been exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting to Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
