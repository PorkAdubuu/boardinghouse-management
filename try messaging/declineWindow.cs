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
        public event Action DeclineWindowClosed;

        private DatabaseConnection dbConnection;
        private int tenantId;
        private int paymentId;
        private int billingId;
        private int roomNumber = 0;
        public declineWindow(int tenantId, int paymentId, int billingId)
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
            this.tenantId = tenantId;
            this.paymentId = paymentId;
            this.billingId = billingId;
            tenantIDtext.Text = tenantId.ToString();
            this.CenterToParent();
            // Remove the title bar buttons (including the Close (X) button)
            this.ControlBox = false;

            // Optionally, remove the form's title bar completely
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void declineWindow_Load(object sender, EventArgs e)
        {
            reasonCombo.Items.Add("Can't find reference number");
            reasonCombo.Items.Add("Insufficient Amount");
            LoadRoomNumber();
            paymentAmountText.Visible = false;
        }
        private void LoadRoomNumber()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                string roomQuery = "SELECT roomnumber FROM tenants_details WHERE tenid = @tenantId";
                MySqlCommand roomCmd = new MySqlCommand(roomQuery, conn);
                roomCmd.Parameters.AddWithValue("@tenantId", tenantId);

                try
                {
                    conn.Open();
                     // Declare as int to match the database type
                    using (MySqlDataReader roomReader = roomCmd.ExecuteReader())
                    {
                        if (roomReader.Read())
                        {
                            roomNumber = roomReader.GetInt32("roomnumber"); // Retrieve as int
                            roomNumberText.Text = roomNumber.ToString(); // Display the room number in the text box
                        }
                        else
                        {
                            MessageBox.Show("Room number not found for the selected tenant.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            roomNumberText.Text = string.Empty; // Clear the text box
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading the room number: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
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

            string tenantEmail;

            // Fetch tenant email
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                conn.Open();

                string fetchEmailQuery = "SELECT email FROM tenants_details WHERE tenid = @tenantID";
                MySqlCommand cmdFetchEmail = new MySqlCommand(fetchEmailQuery, conn);
                cmdFetchEmail.Parameters.AddWithValue("@tenantID", tenantId);

                tenantEmail = cmdFetchEmail.ExecuteScalar()?.ToString();

                if (string.IsNullOrEmpty(tenantEmail))
                {
                    MessageBox.Show("Tenant email not found. Cannot proceed with the decline.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Send email notification for payment decline
            bool emailSent = await SendDeclineEmail(selectedReason, tenantEmail);

            if (!emailSent)
            {
                MessageBox.Show("Email notification failed. Decline process aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
                
            }

            // Proceed with decline process if email was sent successfully
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

                        // Step 5: Handle insufficient amount case
                        if (selectedReason == "Insufficient Amount")
                        {
                            // Fetch total bill
                            string getTotalBillQuery = @"
                            SELECT total_bill 
                            FROM billing_table
                            WHERE billing_id = @billingId AND tenant_id = @tenantId";
                            MySqlCommand cmdGetTotalBill = new MySqlCommand(getTotalBillQuery, conn, transaction);
                            cmdGetTotalBill.Parameters.AddWithValue("@billingId", billingId);
                            cmdGetTotalBill.Parameters.AddWithValue("@tenantId", tenantId);
                            decimal totalBill = Convert.ToDecimal(cmdGetTotalBill.ExecuteScalar());

                            decimal newTotalBill = totalBill - paymentAmount;

                            // Update the amount_paid
                            string updateBillingQuery = @"
                            UPDATE billing_table 
                            SET amount_paid = amount_paid + @paymentAmount 
                            WHERE billing_id = @billingId AND tenant_id = @tenantId";
                            MySqlCommand cmdUpdateBilling = new MySqlCommand(updateBillingQuery, conn, transaction);
                            cmdUpdateBilling.Parameters.AddWithValue("@paymentAmount", paymentAmount);
                            cmdUpdateBilling.Parameters.AddWithValue("@billingId", billingId);
                            cmdUpdateBilling.Parameters.AddWithValue("@tenantId", tenantId);
                            cmdUpdateBilling.ExecuteNonQuery();

                            // Check if the payment already exists in the payment_archive_table
                            string checkPaymentQuery = @"
                            SELECT COUNT(*) 
                            FROM payment_archive_table 
                            WHERE billing_id = @billingId AND tenant_id = @tenantId";

                            MySqlCommand cmdCheckPayment = new MySqlCommand(checkPaymentQuery, conn, transaction);
                            cmdCheckPayment.Parameters.AddWithValue("@billingId", billingId);
                            cmdCheckPayment.Parameters.AddWithValue("@tenantId", tenantId);
                            int paymentCount = Convert.ToInt32(cmdCheckPayment.ExecuteScalar());

                            if (paymentCount > 0)
                            {
                                // Archive the payment and update the due_date if payment already exists
                                string archivePaymentQuery = @"
                                UPDATE payment_archive_table
                                SET 
                                    total_bill = IFNULL(total_bill, 0) + @paymentAmount,  -- Add paymentAmount if total_bill exists, or set it if total_bill is NULL
                                    due_date = (SELECT due_date FROM billing_table WHERE billing_id = @billingId)
                                WHERE billing_id = @billingId AND tenant_id = @tenantId";

                                MySqlCommand cmdArchivePayment = new MySqlCommand(archivePaymentQuery, conn, transaction);
                                cmdArchivePayment.Parameters.AddWithValue("@paymentAmount", paymentAmount);
                                cmdArchivePayment.Parameters.AddWithValue("@billingId", billingId);
                                cmdArchivePayment.Parameters.AddWithValue("@tenantId", tenantId);
                                cmdArchivePayment.ExecuteNonQuery();
                            }
                            else
                            {
                                // Insert new record into payment_archive_table if no existing billing_id
                                string insertPaymentQuery = @"
                                INSERT INTO payment_archive_table (billing_id, tenant_id, total_bill, due_date, boardinghouse, room_number)
                                SELECT 
                                    @billingId, 
                                    @tenantId, 
                                    @paymentAmount, 
                                    due_date, 
                                    boardinghouse,  -- Fetch boardinghouse name from billing_table using billing_id
                                    room_number  -- Fetch room_number from billing_table
                                FROM billing_table 
                                WHERE billing_id = @billingId";

                                MySqlCommand cmdInsertPayment = new MySqlCommand(insertPaymentQuery, conn, transaction);
                                cmdInsertPayment.Parameters.AddWithValue("@paymentAmount", paymentAmount);
                                cmdInsertPayment.Parameters.AddWithValue("@billingId", billingId);
                                cmdInsertPayment.Parameters.AddWithValue("@tenantId", tenantId);
                                cmdInsertPayment.ExecuteNonQuery();
                            }




                        }
                        // Insert notification into admin_notif table
                        string insertAdminNotifQuery = @"
                            INSERT INTO admin_notif (notif_type, description, tenant_id, is_read)
                            VALUES (@notifType, @description, @tenantId, 0)";
                        MySqlCommand cmdInsertAdminNotif = new MySqlCommand(insertAdminNotifQuery, conn, transaction);
                        cmdInsertAdminNotif.Parameters.AddWithValue("@notifType", "Payment Declined");
                        cmdInsertAdminNotif.Parameters.AddWithValue("@description", $"Payment from room {roomNumber} has been declined. Please review their payment records.");
                        cmdInsertAdminNotif.Parameters.AddWithValue("@tenantId", tenantId);
                        cmdInsertAdminNotif.ExecuteNonQuery();
                        // Commit the transaction
                        transaction.Commit();

                        // Show success message
                        MessageBox.Show("Payment declined successfully and records updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DeclineWindowClosed?.Invoke();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction in case of error
                        transaction.Rollback();
                        MessageBox.Show("Error processing decline: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
        }
        private async Task<bool> SendDeclineEmail(string selectedReason, string tenantEmail)
        {
            sendingLabel.Text = "Sending email...";
            sendingLabel.Visible = true;
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;

            try
            {
                string subject = "Payment Declined Notification";
                string body = $"Dear Tenant,\n\nYour payment has been declined due to the following reason: {selectedReason}.\n\nPlease address the issue and make the necessary adjustments to complete the payment.\n\nBest regards,\nThe Boarding House Management Team";

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("boardinghouse24@gmail.com", "cjzvmzmwrspxxkxh");
                    smtpClient.EnableSsl = true;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("boardinghouse24@gmail.com");
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = false;
                        mailMessage.To.Add(tenantEmail);

                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send email: " + ex.Message, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                sendingLabel.Visible = false;
                progressBar.Visible = false;
            }
        }

        private void reasonCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the selected reason is "Insufficient Amount"
            if (reasonCombo.SelectedItem != null && reasonCombo.SelectedItem.ToString() == "Insufficient Amount")
            {
                paymentAmountText.Visible = true; // Show the text box
                paidamountText.Visible = true;
            }
            else
            {
                paymentAmountText.Visible = false; // Hide the text box
                paidamountText.Visible= false;
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

        private void declineWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeclineWindowClosed?.Invoke();
        }
    }
}
