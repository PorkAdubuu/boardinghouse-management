using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace try_messaging
{
    public partial class tenant_lists : Form
    {
        private DatabaseConnection dbConnection;
        private int adminID;
        private int selectedTenantId = 0; // Default value is 0, no tenant selected initially

        public tenant_lists(int adminID)
        {
            InitializeComponent();
            this.adminID = adminID;
            dbConnection = new DatabaseConnection();
            this.adminID = GlobalSettings.LoggedInAdminId;

            SetSearchBarPlaceholder();
        }

        private void tenant_lists_Load(object sender, EventArgs e)
        {
         

            sortCombo.Items.AddRange(new string[]
            {
                "Room Number",
                "Move-in Date",
                "Expiration Date",
                "Gender",
                "Age"
            });

            // Set default selection
            sortCombo.SelectedIndex = 0;

            // Load initial tenant details
            LoadTenantDetails();

            LoadHouseCombo();
        }

        private void LoadHouseCombo()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT house_name FROM boarding_houses"; // Adjust the query to get house names

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    houseCombo.Items.Clear(); // Clear existing items

                    while (reader.Read())
                    {
                        houseCombo.Items.Add(reader.GetString("house_name"));
                    }

                    // Optionally, set the first house as the default selected item
                    if (houseCombo.Items.Count > 0)
                    {
                        houseCombo.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading house names: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetSearchBarPlaceholder()
        {
            // Set the placeholder text
            searchBar.Text = "Search by name, room number..."; // Placeholder text
            searchBar.ForeColor = Color.Gray; // Set color for placeholder text
        }
        private void LoadTenantDetails(string sortColumn = "tenid", string houseName = null)
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    // Modify query to include house filter if a house is selected
                    string query = $@"
            SELECT 
            tenid AS 'ID',
            CONCAT(firstname, ' ', lastname) AS 'Full Name',
            age AS 'Age',
            gender AS 'Gender',
            roomnumber AS 'Room Number',
            email AS 'Email',
            contact AS 'Contact Number',
            address AS 'Address',
            emergency_name1 AS 'emergency name1',
            emergency_contact1 AS 'emergency contact1',
            emergency_name2 AS 'emergency name2',
            emergency_contact2 AS 'emergency contact2',
            air_condition AS 'air condition',
            wifi AS 'Wi-fi',
            parking AS 'Parking',
            house_name AS 'House name',
            movein_date AS 'Move-in Date',
            expiration_date AS 'Expiration Date'
            FROM tenants_details
            WHERE (@houseName IS NULL OR house_name = @houseName) 
            ORDER BY {sortColumn} ASC";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@houseName", houseName ?? (object)DBNull.Value); // Use null if no house is selected

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable tenantTable = new DataTable();
                    adapter.Fill(tenantTable);

                    // Bind the DataTable to the DataGridView
                    tenantList.DataSource = tenantTable;

                    // Customize DataGridView (Optional)
                    tenantList.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    tenantList.AllowUserToAddRows = false;
                    tenantList.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tenant details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void sortCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSort = sortCombo.SelectedItem.ToString();
            string sortColumn;

            // Traditional switch statement
            switch (selectedSort)
            {
                case "Room Number":
                    sortColumn = "roomnumber";
                    break;
                case "Move-in Date":
                    sortColumn = "movein_date";
                    break;
                case "Expiration Date":
                    sortColumn = "expiration_date";
                    break;
                case "Gender":
                    sortColumn = "gender";
                    break;
                case "Age":
                    sortColumn = "age";
                    break;               
                default:
                    sortColumn = "tenid"; // Default sorting by ID
                    break;
            }

            // Reload tenant details with selected sort column
            LoadTenantDetails(sortColumn);
        }

        private void search_Btn_Click(object sender, EventArgs e)
        {
            // Get the search term from the searchBar textbox
            string searchTerm = searchBar.Text;

            // Call a method to search the tenants in the database based on the search term
            SearchTenants(searchTerm);
        }

        private void SearchTenants(string searchTerm)
        {
            // Create your SQL query to search the tenants, excluding the profile_picture column
            string query = "SELECT tenid, lastname, firstname, age, roomnumber, email, contact, gender, address, emergency_name1, emergency_name2, emergency_contact1, emergency_contact2, house_name, movein_date, expiration_date FROM tenants_details WHERE lastname LIKE @searchTerm OR firstname LIKE @searchTerm OR roomnumber LIKE @searchTerm";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
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

                    // Display the search results in a DataGridView (example)
                    tenantList.DataSource = dt; // Replace 'yourDataGridView' with your actual DataGridView control
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void delete_Btn_Click(object sender, EventArgs e)
        {
            // Prompt user to confirm deletion
            DialogResult result = MessageBox.Show("Are you sure you want to delete this tenant?", "Confirm Deletion", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Create and show the password prompt form
                PasswordPromptForm passwordPrompt = new PasswordPromptForm();

                if (passwordPrompt.ShowDialog() == DialogResult.OK)
                {
                    // Get the entered password
                    string enteredPassword = passwordPrompt.AdminPassword;

                    if (VerifyAdminPassword(enteredPassword))
                    {
                        // Proceed with the deletion if password is correct
                        DeleteTenant();
                        
                    }
                    else
                    {
                        MessageBox.Show("Incorrect admin password. Deletion aborted.");
                    }
                }
            }
        }
        private bool VerifyAdminPassword(string enteredPassword)
        {
            // Query to retrieve the admin password from the database
            string query = "SELECT password FROM admin_accounts WHERE admin_id = @admin_id";

            // Assuming the logged-in admin's ID is stored in a variable 'adminId'
            int adminId = GlobalSettings.LoggedInAdminId;
            // Replace with actual admin ID

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@admin_id", adminId);

                    string storedPassword = cmd.ExecuteScalar()?.ToString(); // Retrieve the password from database

                    // Compare the entered password with the stored password
                    return enteredPassword == storedPassword;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                    return false;
                }
            }
        }

        private void DeleteTenant()
        {
            // Use the tenant ID that was selected from the DataGrid
            if (selectedTenantId > 0)
            {
                string deleteQuery = "DELETE FROM tenants_details WHERE tenid = @tenantId";

                // First, get the house_id of the tenant being deleted
                int houseId = GetHouseIdForTenant(selectedTenantId);

                using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
                {
                    try
                    {
                        conn.Open();

                        // Begin transaction to ensure both delete and update happen together
                        using (var transaction = conn.BeginTransaction())
                        {
                            MySqlCommand cmd = new MySqlCommand(deleteQuery, conn);
                            cmd.Parameters.AddWithValue("@tenantId", selectedTenantId);
                            cmd.Transaction = transaction;

                            // Execute tenant deletion
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Decrease occupancy for the associated house
                                DecreaseHouseOccupancy(conn, houseId, transaction);

                                // Commit transaction
                                transaction.Commit();

                                MessageBox.Show("Tenant deleted and occupancy updated successfully.");
                                LoadTenantDetails(); // Reload tenant list
                            }
                            else
                            {
                                MessageBox.Show("Tenant not found.");
                                transaction.Rollback(); // Rollback in case of failure
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No tenant selected.");
            }
        }

        private int GetHouseIdForTenant(int tenantId)
        {
            string query = "SELECT house_id FROM tenants_details WHERE tenid = @tenantId";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching the house ID: " + ex.Message);
                    return 0; // Return 0 if no house_id is found
                }
            }
        }

        private void DecreaseHouseOccupancy(MySqlConnection conn, int houseId, MySqlTransaction transaction)
        {
            string updateQuery = "UPDATE boarding_houses SET current_occupancy = current_occupancy - 1 WHERE house_id = @houseId";

            try
            {
                MySqlCommand cmd = new MySqlCommand(updateQuery, conn);
                cmd.Parameters.AddWithValue("@houseId", houseId);
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the house occupancy: " + ex.Message);
                throw; // Rethrow to roll back the transaction
            }
        }



        private void tenantList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is within the data rows, not header row
            if (e.RowIndex >= 0)
            {
                // Get the tenant ID from the clicked row (assuming "ID" is the first column)
                int tenantId = Convert.ToInt32(tenantList.Rows[e.RowIndex].Cells["ID"].Value);

                // Store or use tenantId for deletion
                selectedTenantId = tenantId;  // Store the selected tenant ID in a variable (use it later for deletion)
            }
        }

        private void refresh_Btn_Click(object sender, EventArgs e)
        {
            LoadTenantDetails();
        }

        private void export_Btn_Click(object sender, EventArgs e)
        {
            // Check if there is data in the DataGridView
            if (tenantList.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    // Create a worksheet in the workbook
                    var worksheet = workbook.Worksheets.Add("Tenant List");

                    // Add column headers
                    for (int col = 0; col < tenantList.Columns.Count; col++)
                    {
                        worksheet.Cell(1, col + 1).Value = tenantList.Columns[col].HeaderText;
                    }

                    // Add rows from DataGridView
                    for (int row = 0; row < tenantList.Rows.Count; row++)
                    {
                        for (int col = 0; col < tenantList.Columns.Count; col++)
                        {
                            worksheet.Cell(row + 2, col + 1).Value = tenantList.Rows[row].Cells[col].Value?.ToString();
                        }
                    }

                    worksheet.Row(1).Style.Font.Bold = true; // Bold the header row
                    worksheet.Columns().AdjustToContents(); // Auto-fit the columns
                    // Open a Save File Dialog for user to save the Excel file
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xlsx";
                        saveFileDialog.Title = "Save Excel File";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Save the workbook to the selected file path
                            workbook.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("Data exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchBar_Enter(object sender, EventArgs e)
        {
            if (searchBar.Text == "Search by name, room number...")
            {
                searchBar.Text = "";
                searchBar.ForeColor = Color.Black; // Reset text color to black when typing
            }
        }

        private void searchBar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBar.Text))
            {
                SetSearchBarPlaceholder();
            }
        }

        private void houseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedHouse = houseCombo.SelectedItem.ToString();
            LoadTenantDetails("tenid", selectedHouse); // Reload tenant details filtered by selected house
        }
    }
}
