using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class tenant_paymentMODE : Form
    {
        private int tenantId;  // Tenant ID (logged in tenant's ID)
        private DatabaseConnection dbConnection;
        public tenant_paymentMODE(int tenantId)
        {
            InitializeComponent();
            this.CenterToParent();
            this.tenantId = tenantId;
            dbConnection = new DatabaseConnection();
        }

        private void tenant_paymentMODE_Load(object sender, EventArgs e)
        {
            LoadTenantBills();
            FillRoomNumber();
        }

        private void FillRoomNumber()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    // Query to get the room number based on tenantId
                    string query = "SELECT roomnumber FROM tenants_details WHERE tenid = @tenantId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        // Set the room number in the roomNumberText control
                        roomNumberText.Text = result.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Room number not found for this tenant.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching room number: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadTenantBills()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT total_bill
                    FROM billing_table
                    WHERE tenant_id = @tenantId
                    ORDER BY billing_id DESC LIMIT 1";  // Fetch the latest bill for the tenant

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);  // Pass only tenant_id

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the bill values for the labels, formatted with thousand separators and 2 decimal points                          
                            totalBill.Text = Convert.ToDecimal(reader["total_bill"]).ToString("N2");

                            
                        }
                        else
                        {
                            MessageBox.Show("No billing information found for this tenant.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tenant bills: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void confirm_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate the reference number
                if (string.IsNullOrWhiteSpace(referenceText.Text))
                {
                    MessageBox.Show("Please provide a valid reference number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int roomNumber = Convert.ToInt32(roomNumberText.Text); // Get room number                
                string referenceNumber = referenceText.Text; // Payment reference number

                using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
                {
                    conn.Open();

                    // Start a transaction
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Step 1: Get the latest billing details
                            string getBillingQuery = @"
                            SELECT billing_id, total_bill 
                            FROM billing_table 
                            WHERE tenant_id = @tenantId AND status IN ('No payment', 'Declined')
                            ORDER BY billing_id DESC LIMIT 1";
                            MySqlCommand cmdGetBilling = new MySqlCommand(getBillingQuery, conn, transaction);
                            cmdGetBilling.Parameters.AddWithValue("@tenantId", tenantId);

                            int billingId;
                            decimal totalBill;

                            using (var reader = cmdGetBilling.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    billingId = Convert.ToInt32(reader["billing_id"]);
                                    totalBill = Convert.ToDecimal(reader["total_bill"]);
                                }
                                else
                                {
                                    MessageBox.Show("No unpaid billing information found for this tenant.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Step 2: Insert into payments_table
                            string insertPaymentQuery = @"
                            INSERT INTO payments_table (reference_number, tenant_id, room_number, billing_id)
                            VALUES (@referenceNumber, @tenantId, @roomNumber, @billingId)";
                            MySqlCommand cmdInsertPayment = new MySqlCommand(insertPaymentQuery, conn, transaction);
                            cmdInsertPayment.Parameters.AddWithValue("@referenceNumber", referenceNumber);
                            cmdInsertPayment.Parameters.AddWithValue("@tenantId", tenantId);
                            cmdInsertPayment.Parameters.AddWithValue("@roomNumber", roomNumber);
                            cmdInsertPayment.Parameters.AddWithValue("@billingId", billingId);

                            cmdInsertPayment.ExecuteNonQuery();

                            // Step 3: Log the transaction in tenant_transaction_table
                            string insertTransactionQuery = @"
                            INSERT INTO tenant_transaction_table (tenant_id, total_bill, status, reference_number)
                            VALUES (@tenantId, @totalBill, 'Pending', @referenceNumber)";
                            MySqlCommand cmdInsertTransaction = new MySqlCommand(insertTransactionQuery, conn, transaction);
                            cmdInsertTransaction.Parameters.AddWithValue("@tenantId", tenantId);
                            cmdInsertTransaction.Parameters.AddWithValue("@totalBill", totalBill);
                            cmdInsertTransaction.Parameters.AddWithValue("@referenceNumber", referenceNumber);

                            cmdInsertTransaction.ExecuteNonQuery();

                            // Step 4: Update billing_table status
                            string updateBillingStatusQuery = @"
                            UPDATE billing_table 
                            SET status = 'Pending' 
                            WHERE billing_id = @billingId";
                            MySqlCommand cmdUpdateBillingStatus = new MySqlCommand(updateBillingStatusQuery, conn, transaction);
                            cmdUpdateBillingStatus.Parameters.AddWithValue("@billingId", billingId);

                            cmdUpdateBillingStatus.ExecuteNonQuery();

                            // Commit the transaction
                            transaction.Commit();

                            MessageBox.Show("Payment successfully processed and logged.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            // Rollback on error
                            transaction.Rollback();
                            MessageBox.Show("Error processing payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error confirming payment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
