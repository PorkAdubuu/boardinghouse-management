using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class payment_admin : Form
    {
        private DatabaseConnection dbConnection;
        private Dictionary<int, int> roomToTenantMap = new Dictionary<int, int>();
        private int selectedPaymentId;
        private int selectedTenantId;
        public payment_admin()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();

            airconditionBill.TextChanged += totalBIll_TextChanged;
            wifiBill.TextChanged += totalBIll_TextChanged;
            parkingBill.TextChanged += totalBIll_TextChanged;
            waterBill.TextChanged += totalBIll_TextChanged;
            electricBill.TextChanged += totalBIll_TextChanged;
            rentBill.TextChanged += totalBIll_TextChanged;
        }

        private void payment_admin_Load(object sender, EventArgs e)
        {
            LoadRoomNumbers();
            LoadBillingData();
            if (roomCombo.SelectedItem != null)
            {
                int selectedRoomNumber = Convert.ToInt32(roomCombo.SelectedItem);
                LoadTenantBills(selectedRoomNumber);
            }
            LoadPaymentsTable();
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
        private void LoadPaymentsTable()
        {
            try
            {
                // Create a new DataTable to hold the data
                DataTable dataTable = new DataTable();

                using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
                {
                    conn.Open();

                    // Query to fetch all payment data from payments_table
                    string query = @"
                    SELECT p.payment_id, p.reference_number, p.date_of_payment, p.tenant_id, 
                    p.room_number, p.billing_id, b.total_bill
                    FROM payments_table p
                    INNER JOIN billing_table b ON p.billing_id = b.billing_id
                    ORDER BY p.date_of_payment DESC";  // Fetch all payments, ordered by date

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        // Fill the DataTable with the result of the query
                        adapter.Fill(dataTable);
                    }
                }

                // Bind the DataTable to the DataGridView to display the data
                tenantpaymentsTable.DataSource = dataTable;

                // Optionally, you can format the columns
                tenantpaymentsTable.Columns["payment_id"].HeaderText = "Payment ID";
                tenantpaymentsTable.Columns["reference_number"].HeaderText = "Reference Number";
                tenantpaymentsTable.Columns["date_of_payment"].HeaderText = "Date of Payment";
                tenantpaymentsTable.Columns["tenant_id"].HeaderText = "Tenant ID";
                tenantpaymentsTable.Columns["room_number"].HeaderText = "Room Number";
                tenantpaymentsTable.Columns["billing_id"].HeaderText = "Billing ID";
                tenantpaymentsTable.Columns["total_bill"].HeaderText = "Total Bill";

                // Optionally, format the date column
                tenantpaymentsTable.Columns["date_of_payment"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading payments data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void LoadTenantBills(int roomNumber)
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT air_condition, wifi, parking FROM tenants_details WHERE roomnumber = @roomNumber";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@roomNumber", roomNumber);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set values for aircon, wifi, and parking bills based on the 'YES' values
                            if (reader["air_condition"].ToString().ToUpper() == "YES")
                            {
                                airconditionBill.Text = "100";
                            }
                            else
                            {
                                airconditionBill.Text = "0";
                            }

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

        private void cubicText_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(cubicText.Text, out decimal cubicMeters) && cubicMeters >= 0)
            {
                decimal waterBillAmount = cubicMeters * 18; // 18 is the rate
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
                decimal electricBillAmount = kWh * 15; // 15 is the rate
                electricBill.Text = electricBillAmount.ToString("F2"); // Display with 2 decimal places
            }
            else
            {
                electricBill.Text = "0.00"; // Set to 0 if the input is invalid
            }
        }

        private void confirm_Btn_Click(object sender, EventArgs e)
        {
            
            if (roomCombo.SelectedItem == null || string.IsNullOrEmpty(airconditionBill.Text) ||
            string.IsNullOrEmpty(wifiBill.Text) || string.IsNullOrEmpty(parkingBill.Text) ||
            string.IsNullOrEmpty(waterBill.Text) || string.IsNullOrEmpty(electricBill.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roomNumber = int.Parse(roomCombo.SelectedItem.ToString());
            DateTime start = startDate.Value;
            DateTime end = endDate.Value;
            decimal airconBill = decimal.Parse(airconditionBill.Text);
            decimal wifiBillValue = decimal.Parse(wifiBill.Text);
            decimal parkingBillValue = decimal.Parse(parkingBill.Text);
            decimal waterBillValue = decimal.Parse(waterBill.Text);
            decimal electricBillValue = decimal.Parse(electricBill.Text);
            decimal rentBills = decimal.Parse(rentBill.Text);  // Fixed rent value
            decimal totalBills = rentBills + airconBill + wifiBillValue + parkingBillValue + waterBillValue + electricBillValue;  // Total bill calculation
            int tenantID = int.Parse(tenant_ID.Text);
            totalBIll.Text = totalBills.ToString("F2");  // Display total bill with two decimal places

            if (end <= start)
            {
                MessageBox.Show("End date must be later than start date.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO billing_table 
                            (room_number, start_date, end_date, aircon_bill, wifi_bill, parking_bill, water_bill, electric_bill, rent_bill, total_bill, tenant_id)
                            VALUES (@roomNumber, @startDate, @endDate, @airconBill, @wifiBill, @parkingBill, @waterBill, @electricBill, @rentBill, @totalBill, @tenantID)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@roomNumber", roomNumber);
                    cmd.Parameters.AddWithValue("@startDate", start);
                    cmd.Parameters.AddWithValue("@endDate", end);
                    cmd.Parameters.AddWithValue("@airconBill", airconBill);
                    cmd.Parameters.AddWithValue("@wifiBill", wifiBillValue);
                    cmd.Parameters.AddWithValue("@parkingBill", parkingBillValue);
                    cmd.Parameters.AddWithValue("@waterBill", waterBillValue);
                    cmd.Parameters.AddWithValue("@electricBill", electricBillValue);
                    cmd.Parameters.AddWithValue("@rentBill", rentBills);  // Add rentBill to the query
                    cmd.Parameters.AddWithValue("@totalBill", totalBills);  // Add totalBill to the query
                    cmd.Parameters.AddWithValue("@tenantID", tenantID);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Billing information saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFormFields();
                        LoadBillingData();
                        LoadPaymentsTable();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save billing information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void ClearFormFields()
        {           
            airconditionBill.Text = "";
            wifiBill.Text = "";
            parkingBill.Text = "";
            waterBill.Text = "";
            electricBill.Text = "";
            startDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now;
        }

        private void totalBIll_TextChanged(object sender, EventArgs e)
        {
            // Attempt to parse the values of each billing field
            decimal airconBill = string.IsNullOrEmpty(airconditionBill.Text) ? 0 : decimal.Parse(airconditionBill.Text);
            decimal wifiBillValue = string.IsNullOrEmpty(wifiBill.Text) ? 0 : decimal.Parse(wifiBill.Text);
            decimal parkingBillValue = string.IsNullOrEmpty(parkingBill.Text) ? 0 : decimal.Parse(parkingBill.Text);
            decimal waterBillValue = string.IsNullOrEmpty(waterBill.Text) ? 0 : decimal.Parse(waterBill.Text);
            decimal electricBillValue = string.IsNullOrEmpty(electricBill.Text) ? 0 : decimal.Parse(electricBill.Text);
            decimal rentBills = string.IsNullOrEmpty(rentBill.Text) ? 3500 : decimal.Parse(rentBill.Text); // Rent is already set as default

            // Calculate the total bill
            decimal totalBills = rentBills + airconBill + wifiBillValue + parkingBillValue + waterBillValue + electricBillValue;

            // Update the totalBill textbox in real-time
            totalBIll.Text = totalBills.ToString("F2");
        }


        private void LoadBillingData()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    // SQL query with column aliases
                    string query = @"SELECT 
                                        billing_id AS 'ID', 
                                        room_number AS 'Room Number', 
                                        start_date AS 'Start Date', 
                                        end_date AS 'End Date', 
                                        aircon_bill AS 'Aircon Bill', 
                                        wifi_bill AS 'Wi-Fi Bill', 
                                        parking_bill AS 'Parking Bill', 
                                        water_bill AS 'Water Bill', 
                                        electric_bill AS 'Electric Bill', 
                                        rent_bill AS 'Rent Bill', 
                                        total_bill AS 'Total Bill', 
                                        status AS 'Status' 
                                    FROM billing_table";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    paymentStatus.DataSource = dataTable;

                    tenantpaymentsTable.AutoResizeColumns();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading billing data: " + ex.Message);
                }
            }
        }

        private void tenant_ID_TextChanged(object sender, EventArgs e)
        {

        }

        private void accept_Btn_Click(object sender, EventArgs e)
        {

            
            
            if (selectedPaymentId > 0)
            {
                ArchivePayment(selectedPaymentId);  // Method to archive payment and update status
            }
            else
            {
                MessageBox.Show("Please select a payment to accept.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            LoadBillingData();
            LoadPaymentsTable();
        }
        private void ArchivePayment(int paymentId)
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    // Step 1: Get the related billing_id
                    string selectQuery = "SELECT billing_id FROM payments_table WHERE payment_id = @paymentId";
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@paymentId", paymentId);

                    int billingId = 0;
                    decimal totalBill = 0;

                    using (MySqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            billingId = reader.GetInt32("billing_id");
                        }
                        else
                        {
                            MessageBox.Show("No billing information found for the selected payment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Step 2: Fetch the total_bill from billing_table
                    string totalBillQuery = "SELECT total_bill FROM billing_table WHERE billing_id = @billingId";
                    MySqlCommand totalBillCmd = new MySqlCommand(totalBillQuery, conn);
                    totalBillCmd.Parameters.AddWithValue("@billingId", billingId);

                    object result = totalBillCmd.ExecuteScalar();
                    if (result != null)
                    {
                        totalBill = Convert.ToDecimal(result);
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve the total bill.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Step 3: Process the payment with a transaction
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Archive the payment
                            string archiveQuery = @"
                        INSERT INTO payment_archive_table (payment_id, reference_number, date_of_payment, tenant_id, room_number, billing_id, total_bill)
                        SELECT payment_id, reference_number, date_of_payment, tenant_id, room_number, billing_id, @totalBill
                        FROM payments_table WHERE payment_id = @paymentId";
                            MySqlCommand archiveCmd = new MySqlCommand(archiveQuery, conn, transaction);
                            archiveCmd.Parameters.AddWithValue("@paymentId", paymentId);
                            archiveCmd.Parameters.AddWithValue("@totalBill", totalBill);
                            archiveCmd.ExecuteNonQuery();

                            // Update the status in billing_table
                            string updateQuery = "UPDATE billing_table SET status = 'Paid' WHERE billing_id = @billingId";
                            MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn, transaction);
                            updateCmd.Parameters.AddWithValue("@billingId", billingId);
                            updateCmd.ExecuteNonQuery();

                            // Update the status in tenant_transaction_table to 'Paid'
                            string updateQueryTransaction = "UPDATE tenant_transaction_table SET status = 'Paid' WHERE tenant_id = @tenantId AND status = 'Pending'";
                            MySqlCommand updateCmdTransaction = new MySqlCommand(updateQueryTransaction, conn, transaction);
                            updateCmdTransaction.Parameters.AddWithValue("@tenantId", selectedTenantId);
                            updateCmdTransaction.ExecuteNonQuery();

                            // Delete the payment from payments_table after archiving
                            string deleteQuery = "DELETE FROM payments_table WHERE payment_id = @paymentId";
                            MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn, transaction);
                            deleteCmd.Parameters.AddWithValue("@paymentId", paymentId);
                            deleteCmd.ExecuteNonQuery();

                            // Commit transaction
                            transaction.Commit();
                            MessageBox.Show($"Payment has been accepted, archived successfully, and the total bill was {totalBill:C2}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error processing the payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error accessing the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void tenantpaymentsTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is not a header
            if (e.RowIndex >= 0)
            {
                // Get the clicked row's payment_id (assuming it's in the first column)
                int paymentId = Convert.ToInt32(tenantpaymentsTable.Rows[e.RowIndex].Cells["payment_id"].Value);

                // Get the clicked row's tenant_id (assuming it's in the second column)
                int tenantId = Convert.ToInt32(tenantpaymentsTable.Rows[e.RowIndex].Cells["tenant_id"].Value);

                // Store the payment ID and tenant ID for later use
                selectedPaymentId = paymentId;
                selectedTenantId = tenantId;

                // Optionally, change the row style to highlight the selected row
                foreach (DataGridViewRow row in tenantpaymentsTable.Rows)
                {
                    row.Selected = false;
                }
                tenantpaymentsTable.Rows[e.RowIndex].Selected = true;

                
            }
        }

        private void decline_Btn_Click(object sender, EventArgs e)
        {
            
            // Ensure a row is selected in the table
            if (tenantpaymentsTable.SelectedRows.Count > 0)
            {
                // Get the selected row's payment_id and tenant_id (assuming they're part of the selected row)
                int paymentId = Convert.ToInt32(tenantpaymentsTable.SelectedRows[0].Cells["payment_id"].Value);
                int tenantId = Convert.ToInt32(tenantpaymentsTable.SelectedRows[0].Cells["tenant_id"].Value);

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
                    {
                        conn.Open();

                        // Start a transaction to ensure both operations are executed successfully
                        using (var transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                // Step 1: Update the status in the billing_table to "Declined"
                                string updateBillingStatusQuery = @"
                            UPDATE billing_table 
                            SET status = 'Declined' 
                            WHERE tenant_id = @tenantId AND status = 'Pending'"; // Assuming only pending payments can be declined
                                MySqlCommand cmdUpdateBillingStatus = new MySqlCommand(updateBillingStatusQuery, conn, transaction);
                                cmdUpdateBillingStatus.Parameters.AddWithValue("@tenantId", tenantId);
                                cmdUpdateBillingStatus.ExecuteNonQuery();

                                // Step 2: Update the status in the tenant_transaction_table to "Declined"
                                string updateTransactionStatusQuery = @"
                            UPDATE tenant_transaction_table
                            SET status = 'Declined'
                            WHERE tenant_id = @tenantId AND status = 'Pending'"; // Assuming the status is 'Pending' before it gets declined
                                MySqlCommand cmdUpdateTransactionStatus = new MySqlCommand(updateTransactionStatusQuery, conn, transaction);
                                cmdUpdateTransactionStatus.Parameters.AddWithValue("@tenantId", tenantId);
                                cmdUpdateTransactionStatus.ExecuteNonQuery();

                                // Step 3: Delete the payment record from the payments_table
                                string deletePaymentQuery = @"
                            DELETE FROM payments_table 
                            WHERE payment_id = @paymentId";
                                MySqlCommand cmdDeletePayment = new MySqlCommand(deletePaymentQuery, conn, transaction);
                                cmdDeletePayment.Parameters.AddWithValue("@paymentId", paymentId);
                                cmdDeletePayment.ExecuteNonQuery();

                                // Commit the transaction if both queries are successful
                                transaction.Commit();

                                // Show a success message to the user
                                MessageBox.Show("Payment declined successfully, status updated in both tables.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadPaymentsTable();
                                LoadBillingData();
                                // Optionally, refresh the payments table or the data grid view
                                LoadPaymentsTable();  // Ensure this method reloads the payments table after the change
                            }
                            catch (Exception ex)
                            {
                                // Rollback the transaction in case of any error
                                transaction.Rollback();
                                MessageBox.Show("Error declining payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error processing decline action: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a payment to decline.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
