using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class boardining_houses : Form
    {
        private DatabaseConnection dbConnection;
        private int selectedhouse_id = 0;
        private string selectedPolicyFilePath = string.Empty;
        private byte[] selectedImageBytes; // To hold the image data



        public boardining_houses()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
        }

        private void confirm_Btn_Click(object sender, EventArgs e)
        {
            // Get the text field values
            string house_name = houseNameText.Text.ToUpper();
            string house_no = "Boarding House #" + houseNumText.Text.ToUpper();
            string location = addressText.Text.ToUpper();
            int capacity;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(house_name) || string.IsNullOrWhiteSpace(house_no) ||
                string.IsNullOrWhiteSpace(location) || string.IsNullOrWhiteSpace(capacityText.Text) ||
                selectedImageBytes == null)
            {
                MessageBox.Show("Please fill in all required fields and select an image.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(capacityText.Text, out capacity))
            {
                // Insert the boarding house details into the database
                using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
                {
                    try
                    {
                        conn.Open();

                        string query = @"INSERT INTO boarding_houses 
                                (house_name, house_no, location, capacity, house_image) 
                                VALUES (@house_name, @house_no, @location, @capacity, @house_image)";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@house_name", house_name);
                        cmd.Parameters.AddWithValue("@house_no", house_no);
                        cmd.Parameters.AddWithValue("@location", location);
                        cmd.Parameters.AddWithValue("@capacity", capacity);
                        cmd.Parameters.AddWithValue("@house_image", selectedImageBytes);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Boarding house details added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the input fields
                        houseNameText.Clear();
                        houseNumText.Clear();
                        addressText.Clear();
                        capacityText.Clear();
                        image_preview.Image = null; // Clear the PictureBox
                        selectedImageBytes = null; // Clear the image bytes

                        LoadBoardingHouseDetails();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inserting boarding house details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number for capacity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void boardining_houses_Load(object sender, EventArgs e)
        {
            

            sortCombo.Items.AddRange(new string[]
            {
                "House number",
                "House name",
                "Location",
                "Capacity",
                "Occupancy"
            });

            // Set default selection
            sortCombo.SelectedIndex = 0;

            LoadBoardingHouseDetails();
        }
        private void LoadBoardingHouseDetails(string sortColumn = "house_id")
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    // Modify the query to dynamically include the ORDER BY clause based on the sortColumn parameter
                    string query = $@"
                SELECT       
                    house_id AS 'ID',
                    house_no AS 'House Number',
                    house_name AS 'House Name',                       
                    location AS 'Location', 
                    capacity AS 'Capacity',
                    current_occupancy AS 'Occupancy'
                FROM boarding_houses
                ORDER BY {sortColumn}"; // Sorting dynamically by the selected column

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable houseTable = new DataTable();

                    // Fill DataTable with query result
                    adapter.Fill(houseTable);

                    // Bind the DataTable to the DataGridView
                    houseList.DataSource = houseTable;

                    // Customize DataGridView (Optional)
                    houseList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    houseList.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    houseList.AllowUserToAddRows = false;
                    houseList.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading boarding house details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                case "House number":
                    sortColumn = "house_no";
                    break;
                case "House name":
                    sortColumn = "house_name";
                    break;
                case "Location":
                    sortColumn = "location";
                    break;
                case "Capacity":
                    sortColumn = "capacity";
                    break;
                case "Occupancy":
                    sortColumn = "current_occupancy";
                    break;
                default:
                    sortColumn = "house_id"; // Default sorting by ID
                    break;                   
            }
            LoadBoardingHouseDetails(sortColumn);
        }

        private void search_Btn_Click(object sender, EventArgs e)
        {
            string searchTerm = searchBar.Text;

            SearchHouses(searchTerm);
        }

        private void SearchHouses(string searchTerm)
        {
            
            string query = "SELECT house_no, house_name, location, capacity, current_occupancy FROM boarding_houses WHERE house_no LIKE @searchTerm OR house_name LIKE @searchTerm OR location LIKE @searchTerm OR capacity LIKE @searchTerm OR current_occupancy LIKE @searchTerm";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    // Execute the query and store the result
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    // Display the search results in a DataGridView (example)
                    houseList.DataSource = dataTable; // Replace 'yourDataGridView' with your actual DataGridView control
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
            DialogResult result = MessageBox.Show("Are you sure you want to delete this house?", "Confirm Deletion", MessageBoxButtons.YesNo);

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
                        DeleteHouse();
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

        private void DeleteHouse()
        {
            
            if (selectedhouse_id > 0)
            {
                string deleteQuery = "DELETE FROM boarding_houses WHERE house_id = @house_id";

                using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
                {
                    try
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(deleteQuery, conn);
                        cmd.Parameters.AddWithValue("@house_id", selectedhouse_id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("House deleted successfully.");
                            // Optionally, refresh the display or data grid view
                            LoadBoardingHouseDetails(); 
                        }
                        else
                        {
                            MessageBox.Show("House not found.");
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
                MessageBox.Show("No house selected.");
            }
        }

        private void houseList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is within the data rows, not header row
            if (e.RowIndex >= 0)
            {
                
                int house_id = Convert.ToInt32(houseList.Rows[e.RowIndex].Cells["ID"].Value);

                // Store or use house_id for deletion
                selectedhouse_id = house_id;  
            }
        }

        private void refresh_Btn_Click(object sender, EventArgs e)
        {
            LoadBoardingHouseDetails();
        }

        private void export_Btn_Click(object sender, EventArgs e)
        {
            // Check if there is data in the DataGridView
            if (houseList.Rows.Count == 0)
            {
                MessageBox.Show("No data available to export.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    // Create a worksheet in the workbook
                    var worksheet = workbook.Worksheets.Add("House List");

                    // Add column headers
                    for (int col = 0; col < houseList.Columns.Count; col++)
                    {
                        worksheet.Cell(1, col + 1).Value = houseList.Columns[col].HeaderText;
                    }

                    // Add rows from DataGridView
                    for (int row = 0; row < houseList.Rows.Count; row++)
                    {
                        for (int col = 0; col < houseList.Columns.Count; col++)
                        {
                            worksheet.Cell(row + 2, col + 1).Value = houseList.Rows[row].Cells[col].Value?.ToString();
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

        private void policy_Btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Files|*.pdf"; // Restrict to PDF files
            openFileDialog.Title = "Select a Policy PDF File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the file name and size
                string fileName = openFileDialog.FileName;
                long fileSize = new FileInfo(fileName).Length;

                // Display the file name and size in a label (or text box)
                policyFileLabel.Text = $"File Name: {Path.GetFileName(fileName)} | File Size: {fileSize / 1024} KB";

            }
        }

        private void image_Btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png"; // Restrict to image files
            openFileDialog.Title = "Select a Boarding House Image";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected image file path
                string fileName = openFileDialog.FileName;

                // Load the image into the PictureBox for preview
                image_preview.Image = Image.FromFile(fileName);

                // Store the image as a byte array to insert into the database later
                byte[] imageBytes = File.ReadAllBytes(fileName);

                // Save the image bytes for insertion (e.g., store in a variable)
                // Ensure you have a global variable to hold this data
                selectedImageBytes = imageBytes;

               
            }
        }
    }
}
