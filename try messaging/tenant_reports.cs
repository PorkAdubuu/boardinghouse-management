using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class tenant : Form
    {
        private DatabaseConnection dbConnection;

        public tenant()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
        }

        private void tenant_reports_Load(object sender, EventArgs e)
        {
            LoadBoardingHouses();
            LoadTenantsArchive();
            if (houseCombo.Items.Count > 0)
            {
                houseCombo.SelectedIndex = 0; // Select the first item
            }

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy"; // Display Year only
            dateTimePicker1.ShowUpDown = true; // Removes the calendar dropdown

            
        }
        private void UpdateArchiveCount(string houseNameFilter = "", string searchText = "", int? yearFilter = null)
        {
            string query = "SELECT COUNT(*) FROM tenants_archive WHERE 1=1";

            if (!string.IsNullOrEmpty(houseNameFilter))
            {
                query += " AND house_name = @houseName";
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                query += " AND (tenid LIKE @searchText OR lastname LIKE @searchText OR firstname LIKE @searchText)";
            }

            if (yearFilter.HasValue)
            {
                query += " AND YEAR(archived_date) = @year";
            }

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(houseNameFilter))
                        {
                            cmd.Parameters.AddWithValue("@houseName", houseNameFilter);
                        }

                        if (!string.IsNullOrEmpty(searchText))
                        {
                            cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                        }

                        if (yearFilter.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@year", yearFilter.Value);
                        }

                        // Execute the query and get the count
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        archiveCountText.Text = $"{count}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error counting tenants' archive: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadBoardingHouses()
        {
            string query = "SELECT DISTINCT house_name FROM boarding_houses";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            houseCombo.Items.Clear();
                            while (reader.Read())
                            {
                                houseCombo.Items.Add(reader["house_name"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading boarding houses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadTenantsArchive(string houseNameFilter = "", string searchText = "", int? yearFilter = null)
        {
            UpdateArchiveCount(houseNameFilter, searchText, yearFilter);
            List<string> columns = new List<string>
            {
                "tenid AS 'Tenant ID'",
                "lastname AS 'Last Name'",
                "firstname AS 'First Name'",
                "age AS 'Age'",
                "birth_date AS 'Birth Date'",
                "roomnumber AS 'Room Number'",
                "email AS 'Email'",
                "contact AS 'Contact Number'",
                "gender AS 'Gender'",
                "emergency_contact1 AS 'Emergency Contact 1'",
                "emergency_contact2 AS 'Emergency Contact 2'",
                "address AS 'Address'",
                "emergency_name1 AS 'Emergency Name 1'",
                "emergency_name2 AS 'Emergency Name 2'",
                "movein_date AS 'Move-in Date'",
                "expiration_date AS 'Expiration Date'",
                "house_name AS 'House Name'",
                "archived_date AS 'Archived Date'",
            };

            string columnsQuery = string.Join(", ", columns);
            string query = $"SELECT {columnsQuery} FROM tenants_archive WHERE 1=1";

            if (!string.IsNullOrEmpty(houseNameFilter))
            {
                query += " AND house_name = @houseName";
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                query += " AND (tenid LIKE @searchText OR lastname LIKE @searchText OR firstname LIKE @searchText)";
            }

            if (yearFilter.HasValue)
            {
                query += " AND YEAR(archived_date) = @year";
            }

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(houseNameFilter))
                        {
                            cmd.Parameters.AddWithValue("@houseName", houseNameFilter);
                        }

                        if (!string.IsNullOrEmpty(searchText))
                        {
                            cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                        }

                        if (yearFilter.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@year", yearFilter.Value);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            archiveDataGrid.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tenants' archive: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void houseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedHouseName = houseCombo.SelectedItem?.ToString() ?? "";
            int selectedYear = dateTimePicker1.Value.Year;
            string searchText = searchBar.Text;

            LoadTenantsArchive(selectedHouseName, searchText, selectedYear);
        }

        private void searchBar_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchBar.Text;
            string selectedHouseName = houseCombo.SelectedItem?.ToString() ?? "";
            int selectedYear = dateTimePicker1.Value.Year;

            LoadTenantsArchive(selectedHouseName, searchText, selectedYear);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int selectedYear = dateTimePicker1.Value.Year;
            string selectedHouseName = houseCombo.SelectedItem?.ToString() ?? "";
            string searchText = searchBar.Text;

            LoadTenantsArchive(selectedHouseName, searchText, selectedYear);
        }

        private void export_Btn_Click(object sender, EventArgs e)
        {
            if (archiveDataGrid.Rows.Count > 0)
            {
                try
                {
                    // Create a SaveFileDialog to ask for the file path
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                        saveFileDialog.Title = "Save Excel File";
                        saveFileDialog.FileName = "Tenant_Archive_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Save the file to the selected path
                            ExportToExcel(saveFileDialog.FileName);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No data available to export.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportToExcel(string filePath)
        {
            try
            {
                // Create an Excel Application
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workBook = excelApp.Workbooks.Add(Type.Missing);
                var workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;
                workSheet.Name = "Tenant Archive";

                // Write column headers and apply bold formatting
                for (int i = 1; i <= archiveDataGrid.Columns.Count; i++)
                {
                    var headerCell = workSheet.Cells[1, i];
                    headerCell.Value = archiveDataGrid.Columns[i - 1].HeaderText;
                    headerCell.Font.Bold = true; // Make headers bold
                }

                // Write rows
                for (int i = 0; i < archiveDataGrid.Rows.Count; i++)
                {
                    for (int j = 0; j < archiveDataGrid.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1] = archiveDataGrid.Rows[i].Cells[j].Value?.ToString() ?? string.Empty;
                    }
                }

                // Save the Excel file
                workBook.SaveAs(filePath);
                workBook.Close();
                excelApp.Quit();

                // Release COM objects
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                MessageBox.Show("Data exported successfully to Excel.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
