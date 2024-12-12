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
    public partial class tenant_account_profile : Form
    {
        private DatabaseConnection dbConnection;
        private int tenantId;
        public tenant_account_profile(int tenantId)
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
            this.tenantId = tenantId;

            addressLabel.BorderStyle = BorderStyle.None;
            boardingAddText.BorderStyle = BorderStyle.None;
            
        }

        private void tenant_account_profile_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            LoadTenantInformation();
            LoadHOuseInformation();
            LoadBillingStatistics();
        }

        private void LoadBillingStatistics()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    // Query to get total bills per month for the given tenant in the current year
                    string query = @"
                SELECT 
                    MONTH(start_date) AS BillMonth, 
                    SUM(total_bill) AS TotalBill
                FROM billing_table
                WHERE 
                    tenant_id = @tenantId 
                    AND YEAR(start_date) = YEAR(CURDATE())
                GROUP BY MONTH(start_date)
                ORDER BY MONTH(start_date)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    // Dictionary to store monthly totals
                    Dictionary<int, decimal> monthlyBills = new Dictionary<int, decimal>();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int month = reader.GetInt32("BillMonth");
                            decimal total = reader.GetDecimal("TotalBill");
                            monthlyBills[month] = total;
                        }
                    }

                    // Populate the chart
                    PopulateChart(monthlyBills);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading billing statistics: " + ex.Message);
                }
            }
        }

        private void PopulateChart(Dictionary<int, decimal> monthlyBills)
        {
            // Clear existing data
            chart1.Series.Clear();
            chart1.Legends.Clear();


            // Create a new series for the chart
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Total Bills",
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column
            };

            // Add data points for each month (January to December)
            for (int month = 1; month <= 12; month++)
            {
                decimal totalBill = monthlyBills.ContainsKey(month) ? monthlyBills[month] : 0;
                series.Points.AddXY(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month), totalBill);
            }

            // Add the series to the chart
            chart1.Series.Add(series);

            // Set chart title (optional)
            chart1.Titles.Clear();
            

            // Customize the chart (optional)
            
            chart1.ChartAreas[0].AxisX.Interval = 1;
        }




        private void LoadTenantInformation()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM tenants_details WHERE tenid = @tenantId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Combine first name and last name and set it in nameLabel
                            nameLabel.Text = $"{reader["lastname"].ToString().ToUpper()}, {reader["firstname"].ToString().ToUpper()}";
                            ageLabel.Text = reader["age"].ToString().ToUpper();
                            genderLabel.Text = reader["gender"].ToString().ToUpper();
                            addressLabel.Text = reader["address"].ToString().ToUpper();
                            emailLabel.Text = reader["email"].ToString();
                            phoneLabel.Text = reader["contact"].ToString().ToUpper();
                            phone1Label.Text = reader["emergency_contact1"].ToString().ToUpper();
                            phone2Label.Text = reader["emergency_contact2"].ToString().ToUpper();
                            name1Label.Text = reader["emergency_name1"].ToString().ToUpper();
                            name2Label.Text = reader["emergency_name2"].ToString().ToUpper();
                            roomLabel.Text = reader["roomnumber"].ToString().ToUpper(); 
                            wifiText.Text = reader["wifi"].ToString().ToUpper();
                            parkingText.Text = reader["parking"].ToString().ToUpper();
                            moveinLabel.Text = Convert.ToDateTime(reader["movein_date"]).ToString("MM/dd/yyyy");
                            expirationLabel.Text = Convert.ToDateTime(reader["expiration_date"]).ToString("MM/dd/yyyy");

                            // Profile Picture: Check if the profile_picture column is not null
                            if (reader["profile_picture"] != DBNull.Value)
                            {
                                byte[] imageBytes = (byte[])reader["profile_picture"];
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    profilePic.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                // If no profile picture, you can set a default image
                                profilePic.Image = Properties.Resources.DefaultProfile; // Example: Default image
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tenant information not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tenant information: " + ex.Message);
                }
            }
        }

        private void LoadHOuseInformation()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    // Get the house_name of the tenant from tenants_details
                    string query = @"
                SELECT bh.house_name, bh.location 
                FROM boarding_houses bh
                INNER JOIN tenants_details td ON td.house_name = bh.house_name
                WHERE td.tenid = @tenantId";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the house name and location to the corresponding controls
                            houseNameText.Text = reader["house_name"].ToString().ToUpper();
                            boardingAddText.Text = reader["location"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("House information not found for the tenant.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading house information: " + ex.Message);
                }
            }
        }



        private void profilePic_Click(object sender, EventArgs e)
        {

        }
    }
}
