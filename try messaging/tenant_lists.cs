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
            this.tenantList.ClearSelection();

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
            tenantList.ReadOnly = true;
            
            //configure table
            
            

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
            pax_number AS 'PAX number',
            email AS 'Email',
            contact AS 'Contact Number',
            address AS 'Address',
            emergency_name1 AS 'emergency name1',
            emergency_contact1 AS 'emergency contact1',
            emergency_name2 AS 'emergency name2',
            emergency_contact2 AS 'emergency contact2',
            birth_date AS 'Birth date',
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
                    tenantList.AllowUserToDeleteRows = false;
                    tenantList.AllowUserToAddRows = false;
                    tenantList.ReadOnly = true;

                    tenantList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
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
            DialogResult result = MessageBox.Show("Are you sure you want to delete this tenant?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                        // Proceed with the deletion if the password is correct
                        int tenantId = selectedTenantId; // Replace this with your logic to get the selected tenant ID

                        if (tenantId == 0)
                        {
                            MessageBox.Show("Please select a valid tenant to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (CheckRemainingBalance(tenantId))
                        {
                            MessageBox.Show("Tenant cannot be deleted because there are outstanding balances.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Delete the tenant
                        DeleteTenant(tenantId);
                    }
                    else
                    {
                        MessageBox.Show("Incorrect admin password. Deletion aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private bool CheckRemainingBalance(int tenantId)
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM billing_table WHERE tenant_id = @tenantId AND remaining_balance > 0.00";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@tenantId", tenantId);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0; // Return true if there are outstanding balances
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while checking the remaining balance: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true; // Prevent deletion in case of an error
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

        private void DeleteTenant(int tenantId)
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    // Get the house_id for the tenant before deleting
                    string getHouseIdQuery = "SELECT house_id FROM tenants_details WHERE tenid = @tenantId";
                    MySqlCommand cmdGetHouseId = new MySqlCommand(getHouseIdQuery, conn);
                    cmdGetHouseId.Parameters.AddWithValue("@tenantId", tenantId);
                    int houseId = Convert.ToInt32(cmdGetHouseId.ExecuteScalar());

                    if (houseId == 0)
                    {
                        MessageBox.Show("No house found for the tenant. Operation aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Start a transaction to ensure atomicity
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Archive the tenant
                            ArchiveTenant(tenantId);

                            // Decrease the house occupancy
                            DecreaseHouseOccupancy(conn, houseId, transaction);

                            // Delete the tenant record
                            string deleteQuery = "DELETE FROM tenants_details WHERE tenid = @tenantId";
                            using (MySqlCommand cmdDelete = new MySqlCommand(deleteQuery, conn))
                            {
                                cmdDelete.Parameters.AddWithValue("@tenantId", tenantId);
                                cmdDelete.Transaction = transaction;

                                int rowsAffected = cmdDelete.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // Commit the transaction
                                    transaction.Commit();
                                    MessageBox.Show("Tenant archived and deleted successfully. House occupancy updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    // Rollback transaction if tenant delete fails
                                    transaction.Rollback();
                                    MessageBox.Show("Failed to delete the tenant. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction in case of error
                            transaction.Rollback();
                            MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting the tenant: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ArchiveTenant(int tenantId)
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    // Get tenant details
                    string selectQuery = @"
                SELECT 
                    tenid, lastname, firstname, age, birth_date, roomnumber, email, contact, gender,
                    emergency_contact1, emergency_contact2, address, emergency_name1, emergency_name2,
                    movein_date, expiration_date, wifi, parking, house_id, house_name, profile_picture
                FROM tenants_details
                WHERE tenid = @tenantId";

                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@tenantId", tenantId);

                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Prepare archive query
                            string insertQuery = @"
                        INSERT INTO tenants_archive (
                            tenid, lastname, firstname, age, birth_date, roomnumber, email, contact, gender,
                            emergency_contact1, emergency_contact2, address, emergency_name1, emergency_name2,
                            movein_date, expiration_date, wifi, parking, house_id, house_name, profile_picture, reason
                        )
                        VALUES (
                            @tenid, @lastname, @firstname, @age, @birth_date, @roomnumber, @email, @contact, @gender,
                            @emergency_contact1, @emergency_contact2, @address, @emergency_name1, @emergency_name2,
                            @movein_date, @expiration_date, @wifi, @parking, @house_id, @house_name, @profile_picture, @reason
                        )";

                            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);

                            // Add parameters for the archive query
                            insertCmd.Parameters.AddWithValue("@tenid", reader["tenid"]);
                            insertCmd.Parameters.AddWithValue("@lastname", reader["lastname"]);
                            insertCmd.Parameters.AddWithValue("@firstname", reader["firstname"]);
                            insertCmd.Parameters.AddWithValue("@age", reader["age"]);
                            insertCmd.Parameters.AddWithValue("@birth_date", reader["birth_date"]);
                            insertCmd.Parameters.AddWithValue("@roomnumber", reader["roomnumber"]);
                            insertCmd.Parameters.AddWithValue("@email", reader["email"]);
                            insertCmd.Parameters.AddWithValue("@contact", reader["contact"]);
                            insertCmd.Parameters.AddWithValue("@gender", reader["gender"]);
                            insertCmd.Parameters.AddWithValue("@emergency_contact1", reader["emergency_contact1"]);
                            insertCmd.Parameters.AddWithValue("@emergency_contact2", reader["emergency_contact2"]);
                            insertCmd.Parameters.AddWithValue("@address", reader["address"]);
                            insertCmd.Parameters.AddWithValue("@emergency_name1", reader["emergency_name1"]);
                            insertCmd.Parameters.AddWithValue("@emergency_name2", reader["emergency_name2"]);
                            insertCmd.Parameters.AddWithValue("@movein_date", reader["movein_date"]);
                            insertCmd.Parameters.AddWithValue("@expiration_date", reader["expiration_date"]);
                            insertCmd.Parameters.AddWithValue("@wifi", reader["wifi"]);
                            insertCmd.Parameters.AddWithValue("@parking", reader["parking"]);
                            insertCmd.Parameters.AddWithValue("@house_id", reader["house_id"]);
                            insertCmd.Parameters.AddWithValue("@house_name", reader["house_name"]);
                            insertCmd.Parameters.AddWithValue("@profile_picture", reader["profile_picture"]);
                            insertCmd.Parameters.AddWithValue("@reason", "Tenant removed by admin"); // Set a reason for archiving

                            reader.Close(); // Close the reader before executing the insert command

                            insertCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("Tenant details not found for archiving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while archiving the tenant: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw; // Re-throw the exception to handle it in the calling method
                }
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

        private void update_Btn_Click(object sender, EventArgs e)
        {

            // Show the password prompt form
            using (PasswordPromptForm passwordPrompt = new PasswordPromptForm())
            {
                if (passwordPrompt.ShowDialog() == DialogResult.OK)
                {
                    // Get the entered password
                    string adminPassword = passwordPrompt.AdminPassword;

                    // Verify the admin password
                    if (VerifyAdminPassword(adminPassword))
                    {
                        // Proceed with the update logic
                        edit_tenant edit_Tenant = new edit_tenant(selectedTenantId);
                        edit_Tenant.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Invalid admin password. Update canceled.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            
        }

        private void tenantList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is within the data rows, not header row
            if (e.RowIndex >= 0)
            {
                // Get the tenant ID from the clicked row (assuming "ID" is the first column)
                int tenantId = Convert.ToInt32(tenantList.Rows[e.RowIndex].Cells["ID"].Value);

                // Store the selected tenant ID in a variable (use it later for passing to edit form)
                selectedTenantId = tenantId;

                
            }
        }
    }
}
