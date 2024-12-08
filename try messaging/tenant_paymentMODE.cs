﻿using DocumentFormat.OpenXml.Wordprocessing;
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

        public event Action tenant_paymentMODEClosed;
        private int selectedBillingId;
        private int tenantId;  // Tenant ID (logged in tenant's ID)
        private DatabaseConnection dbConnection;
        
        public tenant_paymentMODE(int tenantId, int selectedBillingId)
        {
            InitializeComponent();
            this.CenterToParent();
            this.tenantId = tenantId;
            dbConnection = new DatabaseConnection();
            this.selectedBillingId = selectedBillingId; // Assigned here
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
                    SELECT remaining_balance
                    FROM billing_table
                    WHERE tenant_id = @tenantId AND billing_id = @selectedBillingId";  // Use selectedBillingId here

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);  // Pass tenant_id
                    cmd.Parameters.AddWithValue("@selectedBillingId", selectedBillingId);  // Pass selectedBillingId

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the bill values for the labels, formatted with thousand separators and 2 decimal points
                            totalBill.Text = Convert.ToDecimal(reader["remaining_balance"]).ToString("N2");
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
                decimal paymentAmount;

                // Ensure that payment amount is a valid decimal value
                if (!decimal.TryParse(amountText.Text, out paymentAmount))
                {
                    MessageBox.Show("Please enter a valid payment amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
                {
                    conn.Open();

                    // Start a transaction
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Step 1: Get the billing details using selectedBillingId
                            string getBillingQuery = @"
                            SELECT billing_id, total_bill 
                            FROM billing_table 
                            WHERE tenant_id = @tenantId AND billing_id = @selectedBillingId AND status IN ('No payment', 'Declined')";
                            MySqlCommand cmdGetBilling = new MySqlCommand(getBillingQuery, conn, transaction);
                            cmdGetBilling.Parameters.AddWithValue("@tenantId", tenantId);
                            cmdGetBilling.Parameters.AddWithValue("@selectedBillingId", selectedBillingId);  // Use selectedBillingId here

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
                            INSERT INTO tenant_transaction_table (tenant_id, total_bill, status, reference_number, amount_paid)
                            VALUES (@tenantId, @totalBill, 'Pending', @referenceNumber, @amount_paid)";
                            MySqlCommand cmdInsertTransaction = new MySqlCommand(insertTransactionQuery, conn, transaction);
                            cmdInsertTransaction.Parameters.AddWithValue("@tenantId", tenantId);
                            cmdInsertTransaction.Parameters.AddWithValue("@totalBill", totalBill);
                            cmdInsertTransaction.Parameters.AddWithValue("@referenceNumber", referenceNumber);
                            cmdInsertTransaction.Parameters.AddWithValue("@amount_paid", paymentAmount);  // Use the correctly parsed value

                            cmdInsertTransaction.ExecuteNonQuery();

                            // Step 4: Update billing_table status
                            string updateBillingStatusQuery = @"
                            UPDATE billing_table 
                            SET status = 'Pending' 
                            WHERE billing_id = @billingId";
                            MySqlCommand cmdUpdateBillingStatus = new MySqlCommand(updateBillingStatusQuery, conn, transaction);
                            cmdUpdateBillingStatus.Parameters.AddWithValue("@billingId", billingId);

                            cmdUpdateBillingStatus.ExecuteNonQuery();

                            // Step 5: Insert notification into tenant_notif table with 'is_read' = 0 (unread)
                            string insertNotifQuery = @"
                            INSERT INTO tenant_notif (reason, description, notif_type, tenant_id, is_read)
                            VALUES (NULL, @description, @notifType, @tenantId, 0)";  // Set is_read to 0 for unread
                            MySqlCommand cmdInsertNotif = new MySqlCommand(insertNotifQuery, conn, transaction);
                            cmdInsertNotif.Parameters.AddWithValue("@description", "Your payment is being reviewed and will be processed by the admin.");
                            cmdInsertNotif.Parameters.AddWithValue("@notifType", "Payment Details");
                            cmdInsertNotif.Parameters.AddWithValue("@tenantId", tenantId);

                            cmdInsertNotif.ExecuteNonQuery();

                            // Step 6: Insert notification into admin_notif table
                            string insertAdminNotifQuery = @"
                            INSERT INTO admin_notif (notif_type, description, tenant_id, is_read)
                            VALUES (@notifType, @description, @tenantId, 0)";
                            MySqlCommand cmdInsertAdminNotif = new MySqlCommand(insertAdminNotifQuery, conn, transaction);
                            cmdInsertAdminNotif.Parameters.AddWithValue("@notifType", "Payment Alert");
                            cmdInsertAdminNotif.Parameters.AddWithValue("@description", $"Tenant from room {roomNumber} has submitted a payment. Please review their payment details.");
                            cmdInsertAdminNotif.Parameters.AddWithValue("@tenantId", tenantId);

                            cmdInsertAdminNotif.ExecuteNonQuery();

                            // Commit the transaction
                            transaction.Commit();

                            MessageBox.Show("Payment successfully processed and logged.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tenant_paymentMODEClosed?.Invoke();
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

        private void tenant_paymentMODE_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Trigger the event when the form is closed
            tenant_paymentMODEClosed?.Invoke();
        }
    }
}
