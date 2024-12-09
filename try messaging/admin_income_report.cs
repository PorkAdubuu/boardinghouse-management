using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;



namespace try_messaging
{
    public partial class admin_income_report : Form
    {
        private DatabaseConnection dbConnection;
        private string selectedYearr;
        public admin_income_report()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
        }

        private void admin_income_report_Load(object sender, EventArgs e)
        {
            LoadBoardingHouses();
            houseCombo.SelectedIndex = 0;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMMM yyyy"; // Display Month and Year only
            dateTimePicker1.ShowUpDown = true; // Removes the calendar dropdown

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy"; // Year only
            dateTimePicker2.ShowUpDown = true; // Removes the calendar dropdown
            dateTimePicker2.Value = DateTime.Now; // Set to current date and time

        }

        private void LoadBoardingHouses()
        {
            // Query to get all available boarding houses
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
                            houseCombo.Items.Clear(); // Clear existing items

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

        private void houseCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedHouse = houseCombo.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(selectedHouse))
            {
                LoadPaymentReports(selectedHouse);

                // Update total revenue without date filters
                UpdateTotalRevenue(selectedHouse);
                UpdateChartWithMonthlyRevenue(selectedYearr, selectedHouse);

            }
        }

        private void LoadPaymentReports(string boardingHouse)
        {
            // Query to filter payment reports by the selected boarding house, removing time from the dates
            string query = @"
        SELECT                     
            DATE(date_of_payment) AS 'Date of payment',  -- Remove time component
            room_number AS 'Room',                    
            total_bill AS 'Amount', 
            DATE(archived_date) AS 'Completion Date',  -- Remove time component
            boardinghouse AS 'House name'
        FROM payment_archive_table
        WHERE boardinghouse = @boardinghouse";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@boardinghouse", boardingHouse);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            incomeTable.DataSource = dataTable; // Bind data to DataGridView
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading payment reports: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string selectedHouse = houseCombo.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedHouse))
            {
                MessageBox.Show("Please select a boarding house first.", "No Boarding House Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get the selected month and year from the DateTimePicker
            int selectedMonth = dateTimePicker1.Value.Month;
            int selectedYear = dateTimePicker1.Value.Year;

            // Load payment reports based on filters
            string query = @"
        SELECT 
            date_of_payment AS 'Date of payment', 
            room_number AS 'Room',                   
            total_bill AS 'Amount', 
            archived_date AS 'Completion Date', 
            boardinghouse AS 'House name'
        FROM payment_archive_table
        WHERE boardinghouse = @boardinghouse
        AND MONTH(archived_date) = @month
        AND YEAR(archived_date) = @year";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@boardinghouse", selectedHouse);
                        cmd.Parameters.AddWithValue("@month", selectedMonth);
                        cmd.Parameters.AddWithValue("@year", selectedYear);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            incomeTable.DataSource = dataTable; // Bind data to DataGridView
                        }
                    }

                    // Update total revenue with date filters
                    UpdateTotalRevenue(selectedHouse, selectedMonth, selectedYear);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading filtered payment reports: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateTotalRevenue(string boardingHouse, int? month = null, int? year = null)
        {
            // Base query to calculate the total revenue
            string query = @"
        SELECT SUM(total_bill) AS TotalRevenue
        FROM payment_archive_table
        WHERE boardinghouse = @boardinghouse";

            // Add month and year conditions if they are provided
            if (month.HasValue && year.HasValue)
            {
                query += " AND MONTH(archived_date) = @month AND YEAR(archived_date) = @year";
            }

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@boardinghouse", boardingHouse);

                        if (month.HasValue && year.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@month", month.Value);
                            cmd.Parameters.AddWithValue("@year", year.Value);
                        }

                        object result = cmd.ExecuteScalar();

                        // Update the totalRevenueText label with the calculated value
                        totalRevenueText.Text = result != DBNull.Value && result != null
                            ? $"₱ {Convert.ToDecimal(result):N2}"
                            : "₱ 0.00";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error calculating total revenue: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // Get the selected year
            selectedYearr = dateTimePicker2.Value.Year.ToString();

            // Get the currently selected house
            string selectedHouse = houseCombo.SelectedItem?.ToString();

            // SQL query to filter incomeTable by year and house
            string query = @"
            SELECT 
                date_of_payment AS 'Date of payment', 
                room_number AS 'Room',                   
                total_bill AS 'Amount', 
                archived_date AS 'Completion Date', 
                boardinghouse AS 'House name'
            FROM payment_archive_table
            WHERE YEAR(archived_date) = @year AND boardinghouse = @house";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Add parameters to the query to prevent SQL injection
                        cmd.Parameters.AddWithValue("@year", selectedYearr);
                        cmd.Parameters.AddWithValue("@house", selectedHouse);

                        // Execute the query and load the data into a DataTable
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable resultTable = new DataTable();
                            adapter.Fill(resultTable);

                            // Bind the filtered data to the DataGridView (assume the name is incomeTableView)
                            incomeTable.DataSource = resultTable;

                            // Calculate and update the total revenue
                            decimal totalRevenue = resultTable.AsEnumerable()
                                .Sum(row => row.Field<decimal>("Amount"));

                            totalRevenueText.Text = totalRevenue.ToString("C"); // Format as currency
                        }
                    }

                    // Chart function - Get the total revenue per month
                    UpdateChartWithMonthlyRevenue(selectedYearr, selectedHouse);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error filtering data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateChartWithMonthlyRevenue(string selectedYear, string selectedHouse)
        {
            // Query to get monthly revenue per house for the selected year
            string chartQuery = @"
    SELECT 
        MONTH(archived_date) AS Month,
        SUM(total_bill) AS TotalRevenue
    FROM payment_archive_table
    WHERE YEAR(archived_date) = @year AND boardinghouse = @house
    GROUP BY MONTH(archived_date)
    ORDER BY MONTH(archived_date)";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(chartQuery, conn))
                    {
                        // Add parameters to the query to prevent SQL injection
                        cmd.Parameters.AddWithValue("@year", selectedYear);
                        cmd.Parameters.AddWithValue("@house", selectedHouse);

                        // Execute the query and load the data into a DataTable
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable chartData = new DataTable();
                            adapter.Fill(chartData);

                            // Clear previous chart data
                            chart1.Series.Clear();
                            var series = chart1.Series.Add("Monthly Revenue");
                            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column; // Bar graph

                            // Add data points to the chart
                            foreach (DataRow row in chartData.Rows)
                            {
                                int month = row.Field<int>("Month");
                                decimal revenue = row.Field<decimal>("TotalRevenue");
                                series.Points.AddXY(GetMonthName(month), revenue); // Add data points to the chart
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating chart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Helper function to get the month name from the month number
        private string GetMonthName(int month)
        {
            DateTimeFormatInfo dateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            return dateTimeFormat.GetMonthName(month);
        }

        private void export_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a new PDF document
                PdfDocument pdfDocument = new PdfDocument();
                pdfDocument.Info.Title = "Income Report";

                // Create a page in the document
                PdfPage page = pdfDocument.AddPage();

                // Create a graphics object to draw on the page
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Set the font and start drawing text
                XFont font = new XFont("Arial", 10, XFontStyleEx.Regular);
                XFont boldFont = new XFont("Arial", 12, XFontStyleEx.Bold);

                // Write a title to the PDF
                gfx.DrawString("Income Report", boldFont, XBrushes.Black, new XPoint(250, 40));

                // Set starting position for table
                int yPosition = 80;

                // Draw the table headers
                gfx.DrawString("Date of Payment", boldFont, XBrushes.Black, new XPoint(40, yPosition));
                gfx.DrawString("Room", boldFont, XBrushes.Black, new XPoint(200, yPosition));
                gfx.DrawString("Amount", boldFont, XBrushes.Black, new XPoint(280, yPosition));
                gfx.DrawString("Completion Date", boldFont, XBrushes.Black, new XPoint(380, yPosition));
                gfx.DrawString("House", boldFont, XBrushes.Black, new XPoint(500, yPosition));

                // Move y position down for data rows
                yPosition += 30;

                // Loop through the DataGridView rows to populate the table in the PDF
                foreach (DataGridViewRow row in incomeTable.Rows)
                {
                    if (row.IsNewRow) continue;

                    // Format the "Completion Date" to display only the date (yyyy-MM-dd)
                    string completionDate = Convert.ToDateTime(row.Cells["Completion Date"].Value).ToString("yyyy-MM-dd");

                    // Draw each column's value
                    gfx.DrawString(row.Cells["Date of payment"].Value.ToString(), font, XBrushes.Black, new XPoint(40, yPosition));
                    gfx.DrawString(row.Cells["Room"].Value.ToString(), font, XBrushes.Black, new XPoint(200, yPosition));
                    gfx.DrawString(row.Cells["Amount"].Value.ToString(), font, XBrushes.Black, new XPoint(280, yPosition));
                    gfx.DrawString(completionDate, font, XBrushes.Black, new XPoint(380, yPosition));  // Use formatted date
                    gfx.DrawString(row.Cells["House name"].Value.ToString(), font, XBrushes.Black, new XPoint(500, yPosition));

                    // Move the y position for the next row
                    yPosition += 20;
                }

                // Calculate total revenue
                decimal totalRevenue = 0;
                foreach (DataGridViewRow row in incomeTable.Rows)
                {
                    if (row.IsNewRow) continue;
                    totalRevenue += Convert.ToDecimal(row.Cells["Amount"].Value);
                }

                // Draw total revenue at the bottom of the table
                yPosition += 30;
                gfx.DrawString("Total Revenue: ₱" + totalRevenue.ToString("N2"), boldFont, XBrushes.Black, new XPoint(40, yPosition));

                // Capture chart1 as an image
                using (var bitmap = new Bitmap(chart1.Width, chart1.Height))
                {
                    chart1.DrawToBitmap(bitmap, new Rectangle(0, 0, chart1.Width, chart1.Height));

                    // Save the chart image to a memory stream
                    using (var stream = new MemoryStream())
                    {
                        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Position = 0;  // Reset the stream position before reading

                        // Add chart image to the PDF
                        XImage chartImage = XImage.FromStream(stream);
                        gfx.DrawImage(chartImage, 40, yPosition + 70, 350, 300);  // Adjust position and size as needed
                    }
                }

                // Create a SaveFileDialog to let the user choose the location and filename
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";  // Set the filter for PDF files
                saveFileDialog.DefaultExt = "pdf";  // Default extension is .pdf
                saveFileDialog.FileName = "Income_Report_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";  // Set default file name

                // Show the dialog and check if the user selected a file
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Save the PDF to the selected file
                    pdfDocument.Save(saveFileDialog.FileName);

                    // Show a message box confirming the export
                    MessageBox.Show("PDF saved successfully to: " + saveFileDialog.FileName, "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
