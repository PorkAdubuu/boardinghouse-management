using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class bill_report : Form
    {
        private DatabaseConnection dbConnection;

        public bill_report()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
        }

        private void bill_report_Load(object sender, EventArgs e)
        {
            LoadBoardingHouses();
            LoadBillsData();

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMMM yyyy"; // Display Month and Year only
            dateTimePicker1.ShowUpDown = true; // Removes the calendar dropdown

            // Auto-select the first item in houseCombo when the form loads
            if (houseCombo.Items.Count > 0)
            {
                houseCombo.SelectedIndex = 0;
            }
            if (statusCombo.Items.Count > 0)
            {
                statusCombo.SelectedIndex = 0;
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

        private void LoadBillsData(string houseFilter = "", string statusFilter = "", string dateFilter = "")
        {
            string query = @"
                SELECT 
                    billing_id AS 'Billing ID',
                    room_number AS 'Room Number',
                    start_date AS 'Start Date',
                    end_date AS 'End Date',
                    wifi_bill AS 'WiFi Bill',
                    parking_bill AS 'Parking Bill',
                    water_bill AS 'Water Bill',
                    electric_bill AS 'Electric Bill',
                    rent_bill AS 'Rent Bill',
                    total_bill AS 'Total Bill',
                    amount_paid AS 'Amount Paid',
                    issue_date AS 'Issue Date',
                    due_date AS 'Due Date',
                    remaining_balance AS 'Remaining Balance',
                    status AS 'Status',
                    tenant_id AS 'Tenant ID',
                    boardinghouse AS 'Boarding House'
                FROM billing_table
                WHERE tenant_id IS NOT NULL";

            if (!string.IsNullOrEmpty(houseFilter) && houseFilter != "All")
            {
                query += " AND boardinghouse = @houseFilter";
            }

            if (!string.IsNullOrEmpty(statusFilter))
            {
                if (statusFilter == "No payment")
                {
                    query += " AND status IN ('No payment', 'Pending', 'Decline')";
                }
                else if (statusFilter == "Paid")
                {
                    query += " AND status = 'Paid'";
                }
                else if (statusFilter == "Overdue")
                {
                    query += " AND due_date < NOW() AND status != 'Paid'";
                }
            }

            if (!string.IsNullOrEmpty(dateFilter))
            {
                query += " AND DATE_FORMAT(issue_date, '%Y-%m') = @dateFilter";
            }

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(houseFilter) && houseFilter != "All")
                        {
                            cmd.Parameters.AddWithValue("@houseFilter", houseFilter);
                        }
                        if (!string.IsNullOrEmpty(statusFilter))
                        {
                            // Filter parameters are already added in the query
                        }
                        if (!string.IsNullOrEmpty(dateFilter))
                        {
                            cmd.Parameters.AddWithValue("@dateFilter", dateFilter);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            billsDataGrid.DataSource = dt;

                            UpdateStatusCounts(dt); // Update status counts based on the loaded data
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading billing data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateStatusCounts(DataTable dt)
        {
            int billCounts = dt.Rows.Count;
            int paidCounts = 0;
            int overdueCounts = 0;
            int nopaymentCounts = 0;

            foreach (DataRow row in dt.Rows)
            {
                string status = row["Status"].ToString();

                if (status == "Paid")
                {
                    paidCounts++;
                }
                else if (status == "No payment" || status == "Pending" || status == "Decline")
                {
                    nopaymentCounts++;
                }
                else if (status == "Overdue" && DateTime.TryParse(row["due_date"].ToString(), out DateTime dueDate))
                {
                    if (dueDate < DateTime.Now && status != "Paid")
                    {
                        overdueCounts++;
                    }
                }
            }

            // Update the labels with the counts (no text, just numbers)
            billCount.Text = $"{billCounts}";
            paidCount.Text = $"{paidCounts}";
            overdueCount.Text = $"{overdueCounts}";
            nopaymentCount.Text = $"{nopaymentCounts}";
        }



        private void houseCombo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (houseCombo.SelectedItem != null)
            {
                string selectedHouse = houseCombo.SelectedItem.ToString();
                string statusFilter = statusCombo.SelectedItem?.ToString() ?? string.Empty;
                string dateFilter = dateTimePicker1.Value.ToString("yyyy-MM"); // Get the selected year and month
                LoadBillsData(selectedHouse, statusFilter, dateFilter);
            }
        }

        private void statusCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (houseCombo.SelectedItem != null && statusCombo.SelectedItem != null)
            {
                string selectedHouse = houseCombo.SelectedItem.ToString();
                string statusFilter = statusCombo.SelectedItem.ToString();
                string dateFilter = dateTimePicker1.Value.ToString("yyyy-MM"); // Get the selected year and month
                LoadBillsData(selectedHouse, statusFilter, dateFilter);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (houseCombo.SelectedItem != null)
            {
                string selectedHouse = houseCombo.SelectedItem.ToString();
                string statusFilter = statusCombo.SelectedItem?.ToString() ?? string.Empty;
                string dateFilter = dateTimePicker1.Value.ToString("yyyy-MM"); // Get the selected year and month
                LoadBillsData(selectedHouse, statusFilter, dateFilter);
            }
        }

        private void export_Btn_Click(object sender, EventArgs e)
        {
            if (billsDataGrid.Rows.Count > 0) // Assuming billDataGrid is the DataGridView holding the bill data
            {
                try
                {
                    // Get the selected status from the statusCombo
                    string statusFilter = statusCombo.SelectedItem?.ToString() ?? "All"; // Default to "All" if no selection

                    // Create a SaveFileDialog to ask for the file path
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                        saveFileDialog.Title = "Save Excel File";
                        saveFileDialog.FileName = $"{statusFilter}_Bills_{DateTime.Now:yyyyMMdd}.xlsx";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Save the file to the selected path with the dynamic status title
                            ExportBillsToExcel(saveFileDialog.FileName, statusFilter);
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

        private void ExportBillsToExcel(string filePath, string statusFilter)
        {
            try
            {
                // Create an Excel Application
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workBook = excelApp.Workbooks.Add(Type.Missing);
                var workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;
                workSheet.Name = "Bill Archive";

                // Add title based on the selected status
                string statusTitle = $"List for {statusFilter} Bills";
                workSheet.Cells[1, 1] = statusTitle;
                workSheet.get_Range("A1").Font.Size = 14;
                workSheet.get_Range("A1").Font.Bold = true;

                // Write column headers and apply bold formatting
                for (int i = 1; i <= billsDataGrid.Columns.Count; i++)
                {
                    var headerCell = workSheet.Cells[2, i];
                    headerCell.Value = billsDataGrid.Columns[i - 1].HeaderText;
                    headerCell.Font.Bold = true; // Make headers bold
                }

                // Write rows from the DataGrid to the Excel sheet
                for (int i = 0; i < billsDataGrid.Rows.Count; i++)
                {
                    for (int j = 0; j < billsDataGrid.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 3, j + 1] = billsDataGrid.Rows[i].Cells[j].Value?.ToString() ?? string.Empty;
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
