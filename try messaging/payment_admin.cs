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
        private int selectedBillingId;
        public payment_admin()
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();

            
        }

        private void payment_admin_Load(object sender, EventArgs e)
        {
            
            LoadBillingData();
            LoadBillStatus();
            LoadPaymentsTable();

            this.tenantpaymentsTable.ClearSelection();
            this.billStatusTable.ClearSelection();
            this.paymentLogs.ClearSelection();
        }
        private void DeclineWindow_Closed()
        {
            LoadBillingData();
            LoadBillStatus();
            LoadPaymentsTable();
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

                    // Query to fetch all payment data from payments_table, along with the remaining balance from billing_table
                    string query = @"
            SELECT p.payment_id, p.reference_number, p.date_of_payment, p.tenant_id, 
            p.room_number, p.billing_id, b.total_bill, b.remaining_balance
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
                tenantpaymentsTable.Columns["remaining_balance"].HeaderText = "Remaining Balance"; // Added Remaining Balance column

                // Optionally, format the date column
                tenantpaymentsTable.Columns["date_of_payment"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

                tenantpaymentsTable.AllowUserToAddRows = false;
                tenantpaymentsTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                tenantpaymentsTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                tenantpaymentsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading payments data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                                        wifi_bill AS 'Wi-Fi Bill', 
                                        parking_bill AS 'Parking Bill', 
                                        water_bill AS 'Water Bill', 
                                        electric_bill AS 'Electric Bill', 
                                        rent_bill AS 'Rent Bill', 
                                        total_bill AS 'Total Bill', 
                                        remaining_balance AS 'Remaining Balance',
                                        issue_date AS 'Issue Date',
                                        due_date as 'Due Date',
                                        status AS 'Status' 
                                    FROM billing_table
                                    ORDER BY issue_date DESC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    paymentLogs.DataSource = dataTable;

                    tenantpaymentsTable.AutoResizeColumns();

                    paymentLogs.AllowUserToAddRows = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading billing data: " + ex.Message);
                }
            }
        }

        private void LoadBillStatus()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    // SQL query with column aliases
                    string query = @"SELECT                                        
                                        room_number AS 'Room Number', 
                                        status AS 'Status',
                                        start_date AS 'Start Date', 
                                        end_date AS 'End Date'                                                                                
                                    FROM billing_table
                                    ORDER BY issue_date DESC";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    billStatusTable.DataSource = dataTable;

                    tenantpaymentsTable.AutoResizeColumns();

                    billStatusTable.AllowUserToAddRows = false;
                    billStatusTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    billStatusTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    billStatusTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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

                    // Step 1: Get the related billing_id from payments_table
                    string selectQuery = "SELECT billing_id FROM payments_table WHERE payment_id = @paymentId";
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                    selectCmd.Parameters.AddWithValue("@paymentId", paymentId);

                    int billingId = 0;
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

                    // Step 2: Fetch the total_bill and amount_paid from billing_table
                    string totalBillQuery = "SELECT total_bill, amount_paid, tenant_id FROM billing_table WHERE billing_id = @billingId";
                    MySqlCommand totalBillCmd = new MySqlCommand(totalBillQuery, conn);
                    totalBillCmd.Parameters.AddWithValue("@billingId", billingId);

                    decimal totalBill = 0;
                    decimal amountPaid = 0;
                    int tenantID = 0;

                    using (MySqlDataReader reader = totalBillCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            totalBill = reader.GetDecimal("total_bill");
                            amountPaid = reader.GetDecimal("amount_paid");
                            tenantID = reader.GetInt32("tenant_id");
                        }
                        else
                        {
                            MessageBox.Show("Failed to retrieve total_bill and amount_paid from billing_table.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Calculate the remaining balance
                    decimal remainingBalance = totalBill - amountPaid;

                    // Step 3: Process the payment with a transaction
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Archive the payment with the remaining balance (total_bill - amount_paid)
                            string archiveQuery = @"
                            INSERT INTO payment_archive_table (payment_id, reference_number, date_of_payment, tenant_id, room_number, billing_id, total_bill, due_date)
                            SELECT 
                                p.payment_id, 
                                p.reference_number, 
                                p.date_of_payment, 
                                p.tenant_id, 
                                p.room_number, 
                                p.billing_id, 
                                @remainingBalance,
                                b.due_date  -- Get the due_date from the billing_table
                            FROM payments_table p
                            JOIN billing_table b ON p.billing_id = b.billing_id  -- Join payments_table with billing_table on billing_id
                            WHERE p.payment_id = @paymentId";

                            MySqlCommand archiveCmd = new MySqlCommand(archiveQuery, conn, transaction);
                            archiveCmd.Parameters.AddWithValue("@paymentId", paymentId);
                            archiveCmd.Parameters.AddWithValue("@remainingBalance", remainingBalance);
                            archiveCmd.ExecuteNonQuery();

                            // Update the status in billing_table
                            string updateQuery = "UPDATE billing_table SET status = 'Paid' WHERE billing_id = @billingId";
                            MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn, transaction);
                            updateCmd.Parameters.AddWithValue("@billingId", billingId);
                            updateCmd.ExecuteNonQuery();

                            // Update the amount_paid in billing_table to reflect the total bill
                            string updateAmountPaidQuery = "UPDATE billing_table SET amount_paid = @totalBill WHERE billing_id = @billingId";
                            MySqlCommand updateAmountPaidCmd = new MySqlCommand(updateAmountPaidQuery, conn, transaction);
                            updateAmountPaidCmd.Parameters.AddWithValue("@totalBill", totalBill);
                            updateAmountPaidCmd.Parameters.AddWithValue("@billingId", billingId);
                            updateAmountPaidCmd.ExecuteNonQuery();

                            // Update the status in tenant_transaction_table to 'Paid'
                            string updateTransactionQuery = "UPDATE tenant_transaction_table SET status = 'Paid' WHERE tenant_id = @tenantId AND status = 'Pending'";
                            MySqlCommand updateTransactionCmd = new MySqlCommand(updateTransactionQuery, conn, transaction);
                            updateTransactionCmd.Parameters.AddWithValue("@tenantId", tenantID);
                            updateTransactionCmd.ExecuteNonQuery();

                            // Insert notification into tenant_notif table
                            string insertNotifQuery = @"
                            INSERT INTO tenant_notif (reason, description, notif_type, tenant_id)
                            VALUES (NULL, @description, @notifType, @tenantId)";
                            MySqlCommand cmdInsertNotif = new MySqlCommand(insertNotifQuery, conn, transaction);
                            cmdInsertNotif.Parameters.AddWithValue("@description", "Your payment has been processed.");
                            cmdInsertNotif.Parameters.AddWithValue("@notifType", "Payment Received!");
                            cmdInsertNotif.Parameters.AddWithValue("@tenantId", tenantID);
                            cmdInsertNotif.ExecuteNonQuery();

                            // Delete the payment from payments_table after archiving
                            string deleteQuery = "DELETE FROM payments_table WHERE payment_id = @paymentId";
                            MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn, transaction);
                            deleteCmd.Parameters.AddWithValue("@paymentId", paymentId);
                            deleteCmd.ExecuteNonQuery();

                            // Insert notification into admin_notif table
                            string insertAdminNotifQuery = @"
                            INSERT INTO admin_notif (notif_type, description, tenant_id, is_read)
                            VALUES (@notifType, @description, @tenantId, 0)";
                            MySqlCommand cmdInsertAdminNotif = new MySqlCommand(insertAdminNotifQuery, conn, transaction);
                            cmdInsertAdminNotif.Parameters.AddWithValue("@notifType", "Payment Accepted");
                            cmdInsertAdminNotif.Parameters.AddWithValue("@description", $"Payment has been accepted. Please review their payment records.");
                            cmdInsertAdminNotif.Parameters.AddWithValue("@tenantId", tenantID);
                            cmdInsertAdminNotif.ExecuteNonQuery();

                            // Commit transaction
                            transaction.Commit();
                            MessageBox.Show($"Payment has been accepted, archived successfully, and the remaining balance was {remainingBalance:C2}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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



        private void ArchivePaymentDecline(int paymentId)
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

                    // Step 2: Check if the billing_id already exists in payment_archive_table
                    string checkArchiveQuery = "SELECT COUNT(*) FROM payment_archive_table WHERE billing_id = @billingId";
                    MySqlCommand checkCmd = new MySqlCommand(checkArchiveQuery, conn);
                    checkCmd.Parameters.AddWithValue("@billingId", billingId);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        // If the billing_id already exists, do nothing and return
                        
                        return;
                    }

                    // Step 2.5: Retrieve the room number associated with the tenant_id
                    
                    string roomQuery = "SELECT roomnumber FROM tenants_details WHERE tenid = @tenantId";
                    MySqlCommand roomCmd = new MySqlCommand(roomQuery, conn);
                    roomCmd.Parameters.AddWithValue("@tenantId", selectedTenantId);

                    int roomNumber = 0; // Change to int to match the database type
                    using (MySqlDataReader roomReader = roomCmd.ExecuteReader())
                    {
                        if (roomReader.Read())
                        {
                            roomNumber = roomReader.GetInt32("roomnumber"); // Retrieve as int
                        }
                        else
                        {
                            MessageBox.Show("Room number not found for the selected tenant.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    // Step 3: Process the payment with a transaction
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Archive the payment
                            string archiveQuery = @"
                    INSERT INTO payment_archive_table (payment_id, reference_number, date_of_payment, tenant_id, room_number, billing_id)
                    SELECT payment_id, reference_number, date_of_payment, tenant_id, room_number, billing_id
                    FROM payments_table WHERE payment_id = @paymentId";
                            MySqlCommand archiveCmd = new MySqlCommand(archiveQuery, conn, transaction);
                            archiveCmd.Parameters.AddWithValue("@paymentId", paymentId);
                            archiveCmd.ExecuteNonQuery();

                            // Update the status in billing_table
                            string updateQuery = "UPDATE billing_table SET status = 'Declined' WHERE billing_id = @billingId";
                            MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn, transaction);
                            updateCmd.Parameters.AddWithValue("@billingId", billingId);
                            updateCmd.ExecuteNonQuery();

                            // Update the status in tenant_transaction_table to 'Declined'
                            string updateQueryTransaction = "UPDATE tenant_transaction_table SET status = 'Declined' WHERE tenant_id = @tenantId AND status = 'Pending'";
                            MySqlCommand updateCmdTransaction = new MySqlCommand(updateQueryTransaction, conn, transaction);
                            updateCmdTransaction.Parameters.AddWithValue("@tenantId", selectedTenantId);
                            updateCmdTransaction.ExecuteNonQuery();

                            // Delete the payment from payments_table after archiving
                            string deleteQuery = "DELETE FROM payments_table WHERE payment_id = @paymentId";
                            MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn, transaction);
                            deleteCmd.Parameters.AddWithValue("@paymentId", paymentId);
                            deleteCmd.ExecuteNonQuery();

                          

                            

                            // Insert notification into admin_notif table
                            string insertAdminNotifQuery = @"
                            INSERT INTO admin_notif (notif_type, description, tenant_id, is_read)
                            VALUES (@notifType, @description, @tenantId, 0)";
                            MySqlCommand cmdInsertAdminNotif = new MySqlCommand(insertAdminNotifQuery, conn, transaction);
                            cmdInsertAdminNotif.Parameters.AddWithValue("@notifType", "Payment Declined");
                            cmdInsertAdminNotif.Parameters.AddWithValue("@description", $"Payment from room {roomNumber} has been declined. Please review their payment records.");
                            cmdInsertAdminNotif.Parameters.AddWithValue("@tenantId", selectedTenantId);
                            cmdInsertAdminNotif.ExecuteNonQuery();
                            // Commit transaction
                            transaction.Commit();
                            
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
                // Get the clicked row's payment_id (assuming it's in the "payment_id" column)
                int paymentId = Convert.ToInt32(tenantpaymentsTable.Rows[e.RowIndex].Cells["payment_id"].Value);

                // Get the clicked row's tenant_id (assuming it's in the "tenant_id" column)
                int tenantId = Convert.ToInt32(tenantpaymentsTable.Rows[e.RowIndex].Cells["tenant_id"].Value);

                // Get the clicked row's billing_id (assuming it's in the "billing_id" column)
                int billingId = Convert.ToInt32(tenantpaymentsTable.Rows[e.RowIndex].Cells["billing_id"].Value);

                // Store the selected IDs for later use
                selectedPaymentId = paymentId;
                selectedTenantId = tenantId;
                selectedBillingId = billingId; // New variable to store the billing_id

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
            if (tenantpaymentsTable.SelectedRows.Count > 0)
            {
                // Show a confirmation dialog
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to decline this payment? NOTE: This action cannot be undone.",
                    "Confirm Decline",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Get the selected row's payment_id and tenant_id (assuming they're part of the selected row)
                    int paymentId = Convert.ToInt32(tenantpaymentsTable.SelectedRows[0].Cells["payment_id"].Value);
                    int tenantId = Convert.ToInt32(tenantpaymentsTable.SelectedRows[0].Cells["tenant_id"].Value);

                    // Create an instance of declineWindow and pass the tenantId
                    declineWindow declineWindow = new declineWindow(tenantId, paymentId, selectedBillingId);
                    declineWindow.DeclineWindowClosed += DeclineWindow_Closed;
                    declineWindow.Show();

                    if (selectedPaymentId > 0)
                    {
                        ArchivePaymentDecline(selectedPaymentId);  // Method to archive payment and update status
                    }
                }
                else
                {
                    MessageBox.Show("Payment decline process canceled.", "Cancellation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a payment to Decline.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin_issuebill admin_Issuebill = new admin_issuebill();
            admin_Issuebill.Show();
        }

        private void refresh_Btn_Click(object sender, EventArgs e)
        {
            LoadBillingData();
            LoadBillStatus();
            LoadPaymentsTable();
        }
    }
}
