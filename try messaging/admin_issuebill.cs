using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class admin_issuebill : Form
    {
        private DatabaseConnection dbConnection;
        private Dictionary<int, int> roomToTenantMap = new Dictionary<int, int>();
        private int selectedPaymentId;
        private int selectedTenantId;
        private int cubicRates;
        private int kwhRates;
        public admin_issuebill()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
            this.CenterToParent();


            wifiBill.TextChanged += totalBIll_TextChanged;
            parkingBill.TextChanged += totalBIll_TextChanged;
            waterBill.TextChanged += totalBIll_TextChanged;
            electricBill.TextChanged += totalBIll_TextChanged;
            rentBill.TextChanged += totalBIll_TextChanged;

            
        
        }

        private void admin_issuebill_Load(object sender, EventArgs e)
        {
            LoadRoomNumbers();
            if (roomCombo.SelectedItem != null)
            {
                int selectedRoomNumber = Convert.ToInt32(roomCombo.SelectedItem);
                LoadTenantBills(selectedRoomNumber);
            }
        }

        private void LoadRoomNumbers()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT roomnumber, tenid FROM tenants_details"; // Fetch both roomnumber and tenant ID
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<int> roomNumbers = new List<int>();
                        roomToTenantMap.Clear(); // Clear the map before repopulating it

                        while (reader.Read())
                        {
                            int roomNumber = reader.GetInt32("roomnumber");
                            int tenantId = reader.GetInt32("tenid");

                            roomNumbers.Add(roomNumber);
                            roomToTenantMap[roomNumber] = tenantId; // Map room number to tenant ID
                        }

                        roomCombo.DataSource = roomNumbers;

                        // Auto-select the first item if available
                        if (roomNumbers.Count > 0)
                        {
                            roomCombo.SelectedIndex = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading room numbers: " + ex.Message);
                }
            }
        }

        private void roomCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRoomNumber = (int)roomCombo.SelectedItem;

            // Check if the selected room number is in the map, and set the tenant ID in the textbox
            if (roomToTenantMap.ContainsKey(selectedRoomNumber))
            {
                tenant_ID.Text = roomToTenantMap[selectedRoomNumber].ToString();
            }
            else
            {
                tenant_ID.Clear(); // If no matching tenant found, clear the textbox
            }
        }

        private void cubicText_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(cubicText.Text, out decimal cubicMeters) && cubicMeters >= 0)
            {
                decimal waterBillAmount = cubicMeters * cubicRates; // 18 is the rate
                waterBill.Text = waterBillAmount.ToString("F2"); // Display with 2 decimal places
            }
            else
            {
                waterBill.Text = "0.00"; // Set to 0 if the input is invalid
            }
        }

        private void kWhText_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(kWhText.Text, out decimal kWh) && kWh >= 0)
            {
                decimal electricBillAmount = kWh * kwhRates; // 15 is the rate
                electricBill.Text = electricBillAmount.ToString("F2"); // Display with 2 decimal places
            }
            else
            {
                electricBill.Text = "0.00"; // Set to 0 if the input is invalid
            }
        }

        private async void confirm_Btn_Click(object sender, EventArgs e)
        {
            if (roomCombo.SelectedItem == null || 
            string.IsNullOrEmpty(wifiBill.Text) || string.IsNullOrEmpty(parkingBill.Text) ||
            string.IsNullOrEmpty(waterBill.Text) || string.IsNullOrEmpty(electricBill.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roomNumber = int.Parse(roomCombo.SelectedItem.ToString());
            DateTime start = startDate.Value;
            DateTime end = endDate.Value;
            decimal wifiBillValue = decimal.Parse(wifiBill.Text);
            decimal parkingBillValue = decimal.Parse(parkingBill.Text);
            decimal waterBillValue = decimal.Parse(waterBill.Text);
            decimal electricBillValue = decimal.Parse(electricBill.Text);
            decimal rentBills = decimal.Parse(rentBill.Text);  // Fixed rent value
            decimal totalBills = rentBills + wifiBillValue + parkingBillValue + waterBillValue + electricBillValue;  // Total bill calculation
            int tenantID = int.Parse(tenant_ID.Text);
            totalBIll.Text = totalBills.ToString("F2");  // Display total bill with two decimal places

            if (end <= start)
            {
                MessageBox.Show("End date must be later than start date.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                MySqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // Step 1: Insert billing information
                    string query = @"INSERT INTO billing_table 
                    (room_number, start_date, end_date, wifi_bill, parking_bill, water_bill, electric_bill, rent_bill, total_bill, tenant_id)
                    VALUES (@roomNumber, @startDate, @endDate, @wifiBill, @parkingBill, @waterBill, @electricBill, @rentBill, @totalBill, @tenantID)";
                    MySqlCommand cmd = new MySqlCommand(query, conn, transaction);
                    cmd.Parameters.AddWithValue("@roomNumber", roomNumber);
                    cmd.Parameters.AddWithValue("@startDate", start);
                    cmd.Parameters.AddWithValue("@endDate", end);

                    cmd.Parameters.AddWithValue("@wifiBill", wifiBillValue);
                    cmd.Parameters.AddWithValue("@parkingBill", parkingBillValue);
                    cmd.Parameters.AddWithValue("@waterBill", waterBillValue);
                    cmd.Parameters.AddWithValue("@electricBill", electricBillValue);
                    cmd.Parameters.AddWithValue("@rentBill", rentBills);
                    cmd.Parameters.AddWithValue("@totalBill", totalBills);
                    cmd.Parameters.AddWithValue("@tenantID", tenantID);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        // Step 2: Insert notification into tenant_notif table
                        string insertNotifQuery = @"
                        INSERT INTO tenant_notif (reason, description, notif_type, tenant_id)
                        VALUES (NULL, @description, @notifType, @tenantId)";
                        MySqlCommand cmdInsertNotif = new MySqlCommand(insertNotifQuery, conn, transaction);
                        cmdInsertNotif.Parameters.AddWithValue("@description", "Your bill has been updated. Please pay on time.");
                        cmdInsertNotif.Parameters.AddWithValue("@notifType", "New Bill Alert");
                        cmdInsertNotif.Parameters.AddWithValue("@tenantId", tenantID);
                        cmdInsertNotif.ExecuteNonQuery();

                        // Step 3: Fetch tenant email
                        string fetchEmailQuery = "SELECT email FROM tenants_details WHERE tenid = @tenantID";
                        MySqlCommand cmdFetchEmail = new MySqlCommand(fetchEmailQuery, conn, transaction);
                        cmdFetchEmail.Parameters.AddWithValue("@tenantID", tenantID);
                        string tenantEmail = cmdFetchEmail.ExecuteScalar()?.ToString();

                        if (!string.IsNullOrEmpty(tenantEmail))
                        {
                            // Send email notification
                            await SendEmailToTenantAsync(tenantEmail, roomNumber, start, end, totalBills);
                        }

                        // Commit the transaction
                        transaction.Commit();

                        MessageBox.Show("Billing information saved successfully, and notification sent.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFormFields();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save billing information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async Task SendEmailToTenantAsync(string tenantEmail, int roomNumber, DateTime start, DateTime end, decimal totalBill)
        {
            try
            {
                string subject = "New Billing Update";
                string body = $@"
                Dear Tenant,
            
                We have updated your billing information. Below are the details:
            
                Room Number: {roomNumber}
                Billing Period: {start.ToShortDateString()} - {end.ToShortDateString()}
                Total Bill: PHP {totalBill:F2}
            
                Please make your payment on time to avoid penalties.
            
                Thank you.
            
                Sincerely,
                Boarding House Management";

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)) // Gmail SMTP with port 587
                {
                    smtpClient.Credentials = new NetworkCredential("boardinghouse24@gmail.com", "cjzvmzmwrspxxkxh"); // Email and app password
                    smtpClient.EnableSsl = true; // Enable SSL

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("boardinghouse24@gmail.com");
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = false; // Use plain text
                        mailMessage.To.Add(tenantEmail); // Add recipient email

                        // Send the email asynchronously
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }

                MessageBox.Show("Email notification sent to the tenant.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send email: " + ex.Message, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFormFields()
        {         
            waterBill.Text = "";
            electricBill.Text = "";          
        }

        private void totalBIll_TextChanged(object sender, EventArgs e)
        {
            // Attempt to parse the values of each billing field
            decimal wifiBillValue = string.IsNullOrEmpty(wifiBill.Text) ? 0 : decimal.Parse(wifiBill.Text);
            decimal parkingBillValue = string.IsNullOrEmpty(parkingBill.Text) ? 0 : decimal.Parse(parkingBill.Text);
            decimal waterBillValue = string.IsNullOrEmpty(waterBill.Text) ? 0 : decimal.Parse(waterBill.Text);
            decimal electricBillValue = string.IsNullOrEmpty(electricBill.Text) ? 0 : decimal.Parse(electricBill.Text);
            decimal rentBills = string.IsNullOrEmpty(rentBill.Text) ? 3500 : decimal.Parse(rentBill.Text); // Rent is already set as default

            // Calculate the total bill
            decimal totalBills = rentBills + wifiBillValue + parkingBillValue + waterBillValue + electricBillValue;

            // Update the totalBill textbox in real-time
            totalBIll.Text = totalBills.ToString("F2");
        }

        private void LoadTenantBills(int roomNumber)
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT wifi, parking FROM tenants_details WHERE roomnumber = @roomNumber";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@roomNumber", roomNumber);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set values for wifi, and parking bills based on the 'YES' values
                            
                            if (reader["wifi"].ToString().ToUpper() == "YES")
                            {
                                wifiBill.Text = "700";
                            }
                            else
                            {
                                wifiBill.Text = "0";
                            }

                            if (reader["parking"].ToString().ToUpper() == "YES")
                            {
                                parkingBill.Text = "1000";
                            }
                            else
                            {
                                parkingBill.Text = "0";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Room not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tenant bills: " + ex.Message);
                }
            }
        }

        private void confrimRate_Btn_Click(object sender, EventArgs e)
        {
            // Validate and update cubicRates (Water rate)
            if (decimal.TryParse(rateCubicText.Text, out decimal newCubicRate) && newCubicRate > 0)
            {
                cubicRates = (int)newCubicRate; // Convert to integer (or use decimal if needed)
                rateCubicText.Text = newCubicRate.ToString("F2"); // Display formatted rate
            }
            else
            {
                MessageBox.Show("Invalid water rate. Please enter a positive number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Validate and update kwhRates (Electricity rate)
            if (decimal.TryParse(rateKwhText.Text, out decimal newKwhRate) && newKwhRate > 0)
            {
                kwhRates = (int)newKwhRate; // Convert to integer (or use decimal if needed)
                rateKwhText.Text = newKwhRate.ToString("F2"); // Display formatted rate
            }
            else
            {
                MessageBox.Show("Invalid electricity rate. Please enter a positive number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            UpdateBills();
        }

        private void UpdateBills()
        {
            // Recalculate water bill
            if (decimal.TryParse(cubicText.Text, out decimal cubicMeters) && cubicMeters >= 0)
            {
                decimal waterBillAmount = cubicMeters * cubicRates;
                waterBill.Text = waterBillAmount.ToString("F2");
            }
            else
            {
                waterBill.Text = "0.00";
            }

            // Recalculate electric bill
            if (decimal.TryParse(kWhText.Text, out decimal kWh) && kWh >= 0)
            {
                decimal electricBillAmount = kWh * kwhRates;
                electricBill.Text = electricBillAmount.ToString("F2");
            }
            else
            {
                electricBill.Text = "0.00";
            }
        }

    }
}
