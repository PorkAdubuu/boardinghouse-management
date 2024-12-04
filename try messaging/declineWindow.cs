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
    public partial class declineWindow : Form
    {
        private DatabaseConnection dbConnection;
        private int tenantId;
        private int paymentId;
        private int billingId;
        public declineWindow(int tenantId, int paymentId, int billingId)
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
            this.tenantId = tenantId;
            this.paymentId = paymentId;
            this.billingId = billingId;
            tenantIDtext.Text = tenantId.ToString();
            this.CenterToParent();
        }

        private void declineWindow_Load(object sender, EventArgs e)
        {
            reasonCombo.Items.Add("Can't find reference number");
            reasonCombo.Items.Add("Insufficient Amount");

            paymentAmountText.Visible = false;
        }

        private async void confirmDecline_Btn_Click(object sender, EventArgs e)
        {
            string selectedReason = reasonCombo.SelectedItem?.ToString();
            string description = descriptionText.Text;
            decimal paymentAmount = 0;

            if (string.IsNullOrWhiteSpace(selectedReason))
            {
                MessageBox.Show("Please select a reason for declining the payment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if payment amount is valid for the insufficient amount case
            if (selectedReason == "Insufficient Amount" &&
                (!decimal.TryParse(paymentAmountText.Text, out paymentAmount) || paymentAmount <= 0))
            {
                MessageBox.Show("Please enter a valid payment amount for the insufficient amount case.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                conn.Open();

                // Start a transaction to ensure atomicity
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Step 1: Update the status in the billing_table to "Declined"
                        string updateBillingStatusQuery = @"
        UPDATE billing_table 
        SET status = 'Declined' 
        WHERE tenant_id = @tenantId AND status = 'Pending'";
                        MySqlCommand cmdUpdateBillingStatus = new MySqlCommand(updateBillingStatusQuery, conn, transaction);
                        cmdUpdateBillingStatus.Parameters.AddWithValue("@tenantId", tenantId);
                        cmdUpdateBillingStatus.ExecuteNonQuery();

                        // Step 2: Update the status in the tenant_transaction_table to "Declined"
                        string updateTransactionStatusQuery = @"
        UPDATE tenant_transaction_table
        SET status = 'Declined'
        WHERE tenant_id = @tenantId AND status = 'Pending'";
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

                        // Step 4: Insert into tenant_notif table
                        string insertNotifQuery = @"
        INSERT INTO tenant_notif (reason, description, notif_type, tenant_id)
        VALUES (@reason, @description, @notifType, @tenantId)";
                        MySqlCommand cmdInsertNotif = new MySqlCommand(insertNotifQuery, conn, transaction);
                        cmdInsertNotif.Parameters.AddWithValue("@reason", selectedReason);
                        cmdInsertNotif.Parameters.AddWithValue("@description",
                            selectedReason == "Can't find reference number"
                            ? "The Payment reference number cannot be found, please check and reupload your reference number."
                            : "Please pay the remaining balance.");
                        cmdInsertNotif.Parameters.AddWithValue("@notifType", "Payment Unsuccessful");
                        cmdInsertNotif.Parameters.AddWithValue("@tenantId", tenantId);
                        cmdInsertNotif.ExecuteNonQuery();

                        // Step 5: Fetch tenant email
                        string fetchEmailQuery = "SELECT email FROM tenants_details WHERE tenid = @tenantID";
                        MySqlCommand cmdFetchEmail = new MySqlCommand(fetchEmailQuery, conn, transaction);
                        cmdFetchEmail.Parameters.AddWithValue("@tenantID", tenantId);
                        string tenantEmail = cmdFetchEmail.ExecuteScalar()?.ToString();

                        // Step 6: Send email notification for payment decline
                        await SendDeclineEmail(selectedReason, tenantEmail);  // Pass tenantEmail here

                        // Step 7: Update total_bill in billing_table for insufficient amount case
                        if (selectedReason == "Insufficient Amount")
                        {
                            string getTotalBillQuery = @"
            SELECT total_bill 
            FROM billing_table
            WHERE billing_id = @billingId AND tenant_id = @tenantId";

                            MySqlCommand cmdGetTotalBill = new MySqlCommand(getTotalBillQuery, conn, transaction);
                            cmdGetTotalBill.Parameters.AddWithValue("@billingId", billingId);
                            cmdGetTotalBill.Parameters.AddWithValue("@tenantId", tenantId);
                            decimal totalBill = Convert.ToDecimal(cmdGetTotalBill.ExecuteScalar());

                            decimal newTotalBill = totalBill - paymentAmount;

                            // Update the amount_paid by adding the paymentAmount to the existing amount_paid
                            string updateBillingQuery = @"
            UPDATE billing_table 
            SET amount_paid = amount_paid + @paymentAmount  -- This adds to the current value of amount_paid
            WHERE billing_id = @billingId AND tenant_id = @tenantId";

                            MySqlCommand cmdUpdateBilling = new MySqlCommand(updateBillingQuery, conn, transaction);
                            cmdUpdateBilling.Parameters.AddWithValue("@paymentAmount", paymentAmount);
                            cmdUpdateBilling.Parameters.AddWithValue("@billingId", billingId);
                            cmdUpdateBilling.Parameters.AddWithValue("@tenantId", tenantId);
                            cmdUpdateBilling.ExecuteNonQuery();

                            // Archive the payment in the archive table
                            string archivePaymentQuery = @"
            UPDATE payment_archive_table
            SET total_bill = @paymentAmount
            WHERE billing_id = @billingId AND tenant_id = @tenantId";

                            MySqlCommand cmdArchivePayment = new MySqlCommand(archivePaymentQuery, conn, transaction);
                            cmdArchivePayment.Parameters.AddWithValue("@paymentAmount", paymentAmount);
                            cmdArchivePayment.Parameters.AddWithValue("@billingId", billingId);
                            cmdArchivePayment.Parameters.AddWithValue("@tenantId", tenantId);
                            cmdArchivePayment.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        // Show success message
                        MessageBox.Show("Payment declined successfully and records updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction in case of error
                        transaction.Rollback();
                        MessageBox.Show("Error processing decline: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private async Task SendDeclineEmail(string selectedReason, string tenantEmail)
        {
            try
            {
                string subject = "Payment Declined Notification";
                string body = $"Dear Tenant,\n\nYour payment has been declined due to the following reason: {selectedReason}.\n\nPlease address the issue and make the necessary adjustments to complete the payment.\n\nBest regards,\nThe Boarding House Management Team";
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

        private void reasonCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the selected reason is "Insufficient Amount"
            if (reasonCombo.SelectedItem != null && reasonCombo.SelectedItem.ToString() == "Insufficient Amount")
            {
                paymentAmountText.Visible = true; // Show the text box
            }
            else
            {
                paymentAmountText.Visible = false; // Hide the text box
            }

            // Check the selected reason and update the descriptionText
            if (reasonCombo.SelectedItem != null)
            {
                string selectedReason = reasonCombo.SelectedItem.ToString();

                // Update the description based on the selected reason
                if (selectedReason == "Can't find reference number")
                {
                    descriptionText.Text = "The payment reference number cannot be found. Please check and reupload your reference number.";
                }
                else if (selectedReason == "Insufficient Amount")
                {
                    descriptionText.Text = "The payment amount is insufficient. Please pay the remaining balance.";
                }
                else
                {
                    descriptionText.Text = string.Empty; // Clear description for other cases
                }
            }
        }
    }
}
