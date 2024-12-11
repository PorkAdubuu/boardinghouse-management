using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace try_messaging
{
    public partial class MaintenanceRequestForm : Form
    {
        private int requestId; // Store the request ID to fetch data
        private int requestNumber;
        private admin_maintenance adminForm; // Reference to the admin maintenance form
        private string tenantEmail; // Store the tenant's email
        private string declineReason; // Store the decline reason     

        public MaintenanceRequestForm(int requestId, admin_maintenance adminForm)
        {
            InitializeComponent();
            this.requestId = requestId; // Assign the request ID
            this.adminForm = adminForm; // Store the reference to the admin form
            this.CenterToParent();
        }

        private void MaintenanceRequestForm_Load(object sender, EventArgs e)
        {
            LoadMaintenanceRequestDetails();
            UpdatePanelVisibility();
            

        }
        private void UpdatePanelVisibility()
        {
            // Ensure the statusForm textbox is not null
            if (statusForm == null)
            {
                MessageBox.Show("The statusForm textbox is not initialized.");
                return;
            }

            string status = statusForm.Text.Trim();
            if (!status.Equals("Pending", StringComparison.OrdinalIgnoreCase)) // If status is not "Pending"
            {
                // Non-Pending status - disable and set components to gray
                acceptPanel.Visible = true;  // Panels still visible
                declinePanel.Visible = true;

                // Set panel colors to light gray (if needed, uncomment the following lines)
                // acceptPanel.BackColor = Color.LightGray;
                // declinePanel.BackColor = Color.LightGray;

                // Set label styles
                label15.ForeColor = Color.DarkGray;
                label10.ForeColor = Color.DarkGray;
                label14.ForeColor = Color.DarkGray;
                label11.ForeColor = Color.DarkGray;
                label12.ForeColor = Color.DarkGray;

                // Disable buttons and change appearance
                acceptButton.Enabled = false;
                acceptButton.BackColor = Color.DarkGray;
                acceptButton.Cursor = Cursors.No;

                decline_Button.Enabled = false;
                decline_Button.BackColor = Color.DarkGray;
                decline_Button.Cursor = Cursors.No;

                // Set RichTextBox to read-only and gray
                reasonDecline.ReadOnly = true;
                reasonDecline.BackColor = Color.DarkGray;

                // Disable the DatePicker
                dateInspection.Enabled = false;
            }
            

        }



        private void LoadMaintenanceRequestDetails()
        {
            try
            {
                string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // SQL query to fetch maintenance request details
                    string query = @"SELECT 
                    mr.request_id, 
                    mr.request_number,
                    td.email,  -- Fetch the tenant's email
                    CONCAT(td.lastname, ', ', td.firstname) AS tenant_name, 
                    td.roomnumber, 
                    mr.maintenance_type, 
                    mr.description, 
                    mr.request_date, 
                    mr.status,
                    mr.dateInspection, 
                    mr.image_data  -- Fetch the image data
                FROM 
                    maintenance_requests mr
                INNER JOIN 
                    tenants_details td ON mr.tenant_id = td.tenid
                WHERE 
                    mr.request_id = @requestId";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@requestId", requestId);

                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        requestIdForm.Text = reader["request_id"].ToString();
                        requestNumberForm.Text = reader["request_number"].ToString();
                        tenantEmail = reader["email"].ToString(); // Store tenant's email
                        tenantNameForm.Text = reader["tenant_name"].ToString();
                        roomNumberForm.Text = reader["roomnumber"].ToString();
                        maintenanceTypeForm.Text = reader["maintenance_type"].ToString();
                        descriptionForm.Text = reader["description"].ToString();
                        dateSubmittedForm.Text = Convert.ToDateTime(reader["request_date"]).ToString("yyyy-MM-dd");
                        statusForm.Text = reader["status"].ToString(); // Set the current status in the TextBox

                        // Set the inspection date in the DateTimePicker
                        if (reader["dateInspection"] != DBNull.Value)
                        {
                            dateInspection.Value = Convert.ToDateTime(reader["dateInspection"]);
                        }
                        else
                        {
                            dateInspection.Value = DateTime.Now; // Default to now or handle as needed
                        }

                        // Load the image data
                        if (reader["image_data"] != DBNull.Value)
                        {
                            byte[] imageData = (byte[])reader["image_data"];
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                pictureBox1.Image = Image.FromStream(ms); // Load the image into the PictureBox
                            }
                        }
                        else
                        {
                            pictureBox1.Image = null; // Clear the PictureBox if no image data is found
                            MessageBox.Show("No image data found for this maintenance request.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading maintenance request details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private async Task SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                // Set the security protocol
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Use TLS 1.2

                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)) // Use port 587
                {
                    smtpClient.Credentials = new NetworkCredential("boardinghouse24@gmail.com", "cjzvmzmwrspxxkxh"); // Email and app password
                    smtpClient.EnableSsl = true; // Enable SSL

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("boardinghouse24@gmail.com");
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = false; // HTML or plain text
                        mailMessage.To.Add(toAddress); // Add recipient email

                        // Send the email asynchronously
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.ToString()); // Detailed error message
            }
        }

        private async void acceptButton_Click(object sender, EventArgs e)
        {
            string requestNumber = requestNumberForm.Text;
            // Prevent acceptance if status is 'In Progress'
            if (statusForm.Text == "In Progress")
            {
                MessageBox.Show("This request is already in progress and cannot be accepted again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the selected inspection date
                DateTime inspectionDate = dateInspection.Value; // Assuming dateInspection is a DateTimePicker
                string formattedInspectionDate = inspectionDate.ToString("yyyy-MM-dd"); // Format the date

                string subject = "Maintenance Request Accepted";
                string body = $"Dear Tenant,\n\n" +
                              $"Thank you for submitting your maintenance request (Number: {requestNumber}).\n" +
                              $"We want to inform you that your request has been received\n" +
                              $"and is currently being reviewed by our team.\n\n" +
                              $"We understand the urgency of your request and appreciate your patience.\n\n" +
                              $"We will contact you on {formattedInspectionDate} to schedule\n" +
                              $"a convenient time for inspection. Please be aware that the inspection\n" +
                              $"date indicates when the maintenance person will arrive. They will be responsible\n" +
                              $"for communicating directly with you regarding any concerns and coordinating the visit.\n\n" +
                              $"If you have any questions or need to provide additional information,\n" +
                              $"please don't hesitate to reach out.\n\n" +
                              $"Thank you for your patience.\n\n" +
                              $"Best regards,\n" +
                              $"Your BoardMate Team";


                await SendEmail(tenantEmail, subject, body); // Send acceptance email

                // Insert accepted notification into tenant_notif table
                string insertAcceptedNotifQuery = @"
        INSERT INTO tenant_notif (reason, description, notif_type, notif_date, is_read, tenant_id)
        VALUES (NULL, @description, @notifType, NOW(), 0, @tenantId)";

                using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;"))
                {
                    conn.Open();

                    // Step 1: Retrieve tenantId from maintenance_requests table using requestId
                    string getTenantIdQuery = "SELECT tenant_id FROM maintenance_requests WHERE request_id = @requestId";
                    MySqlCommand cmdGetTenantId = new MySqlCommand(getTenantIdQuery, conn);
                    cmdGetTenantId.Parameters.AddWithValue("@requestId", requestId); // Ensure requestId is defined
                    object tenantIdObj = cmdGetTenantId.ExecuteScalar();

                    if (tenantIdObj != null)
                    {
                        int tenantId = Convert.ToInt32(tenantIdObj);

                        // Step 2: Insert notification into tenant_notif table
                        MySqlCommand cmdInsertNotif = new MySqlCommand(insertAcceptedNotifQuery, conn);
                        cmdInsertNotif.Parameters.AddWithValue("@description", $"Your maintenance request has been accepted. Inspection scheduled on {formattedInspectionDate}.");
                        cmdInsertNotif.Parameters.AddWithValue("@notifType", "Maintenance Request Accepted");
                        cmdInsertNotif.Parameters.AddWithValue("@tenantId", tenantId);
                        cmdInsertNotif.ExecuteNonQuery();
                    }
                    else
                    {
                        Console.WriteLine("Error: Tenant ID not found for the given request ID.");
                    }
                }

                // Set the request status to 'Pending'
                SetRequestStatusPending(requestId);

                // Update request status in the database
                UpdateRequestStatus("In progress");

                MessageBox.Show("Maintenance request accepted and email sent.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMaintenanceRequestDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accepting maintenance request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetRequestStatusPending(int requestId)
        {
            try
            {
                string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Update the status column to 'Pending'
                    string updateQuery = @"UPDATE maintenance_requests 
                                   SET status = 'Pending' 
                                   WHERE request_id = @requestId";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@requestId", requestId);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Request ID {requestId}: Status set to 'Pending'.");
                    }
                    else
                    {
                        Console.WriteLine($"Request ID {requestId}: No records updated. Check if the ID is valid.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error setting request status to Pending: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void decline_Button_Click(object sender, EventArgs e)
        {
            string requestNumber = requestNumberForm.Text;
            string declineReason = reasonDecline.Text;
            // Prevent decline if status is 'In Progress'
            if (statusForm.Text == "In Progress")
            {
                MessageBox.Show("This request is already in progress and cannot be declined.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Use the decline reason from the text box
            string subject = "Maintenance Request Declined";
            string body = $"Dear Tenant,\n\n" +
                          $"Thank you for your recent maintenance request (Number: {requestNumber}). We appreciate\n" +
                          $"your approach in reporting maintenance concerns.\n\n" +
                          $"However, after careful consideration, we regret to inform you that we cannot\n" +
                          $"proceed with your request due to the following reason:\n" +
                          $"{declineReason}\n\n" + // Use the decline reason from the field
                          $"We encourage you to submit another maintenance request for any other issues or\n" +
                          $"concerns you may have. Your comfort and satisfaction are our top priorities, and\n" +
                          $"we want to ensure that all necessary issues are addressed promptly.\n\n" +
                          $"Thank you for understanding, and we look forward to assisting you with\n" +
                          $"future maintenance requests.\n\n" +
                          $"Best regards,\n" +
                          $"Your BoardMate Team";

            await SendEmail(tenantEmail, subject, body); // Send decline email

            // Insert declined notification into tenant_notif table
            string insertDeclinedNotifQuery = @"
                INSERT INTO tenant_notif (reason, description, notif_type, notif_date, is_read, tenant_id)
                VALUES (NULL, @description, @notifType, NOW(), 0, @tenantId)";

            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;"))
            {
                conn.Open();

                // Step 1: Retrieve tenantId from maintenance_requests table using requestId
                string getTenantIdQuery = "SELECT tenant_id FROM maintenance_requests WHERE request_id = @requestId";
                MySqlCommand cmdGetTenantId = new MySqlCommand(getTenantIdQuery, conn);
                cmdGetTenantId.Parameters.AddWithValue("@requestId", requestId); // Ensure requestId is defined
                object tenantIdObj = cmdGetTenantId.ExecuteScalar();

                if (tenantIdObj != null)
                {
                    int tenantId = Convert.ToInt32(tenantIdObj);

                    // Step 2: Insert notification into tenant_notif table
                    MySqlCommand cmdInsertNotif = new MySqlCommand(insertDeclinedNotifQuery, conn);
                    cmdInsertNotif.Parameters.AddWithValue("@description", $"Your maintenance request has been declined. Please submit another maintenance request.");
                    cmdInsertNotif.Parameters.AddWithValue("@notifType", "Maintenance Request Declined");
                    cmdInsertNotif.Parameters.AddWithValue("@tenantId", tenantId);
                    cmdInsertNotif.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("Error: Tenant ID not found for the given request ID.");
                }
            }

            // Update request status in the database
            SetStatusToDeclined(requestId);

            MessageBox.Show("Maintenance request declined and email sent.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadMaintenanceRequestDetails();
        }
        private void SetStatusToDeclined(int requestId)
        {
            try
            {
                string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Check current status
                    string checkStatusQuery = @"SELECT status FROM maintenance_requests WHERE request_id = @requestId";
                    MySqlCommand checkStatusCommand = new MySqlCommand(checkStatusQuery, connection);
                    checkStatusCommand.Parameters.AddWithValue("@requestId", requestId);

                    object currentStatusObj = checkStatusCommand.ExecuteScalar();
                    if (currentStatusObj != null)
                    {
                        string currentStatus = currentStatusObj.ToString();
                        if (currentStatus == "Declined")
                        {
                            MessageBox.Show("This request is already marked as 'Declined'.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Request ID not found. Please check if the ID is valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update the status column to 'Declined'
                    string updateQuery = @"UPDATE maintenance_requests 
                                   SET status = 'Declined' 
                                   WHERE request_id = @requestId";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@requestId", requestId);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("The request status has been successfully updated to 'Declined'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No records were updated. Please check if the request ID is valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error setting request status to 'Declined': " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateRequestStatus(string status)
        {
            try
            {
                string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if a record exists for the given request_id
                    string checkQuery = @"SELECT COUNT(*) FROM maintenance_requests WHERE request_id = @requestId";
                    MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@requestId", requestId);

                    int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (recordCount == 0)
                    {
                        // No record exists with the given request_id; throw an error or handle accordingly
                        MessageBox.Show("Error: Maintenance request not found for the given Request ID.",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Record exists; perform an UPDATE
                    string updateQuery = @"UPDATE maintenance_requests 
                                   SET status = @status, 
                                       dateInspection = @inspectionDate 
                                   WHERE request_id = @requestId";
                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@status", status);
                    updateCommand.Parameters.AddWithValue("@inspectionDate", dateInspection.Value);
                    updateCommand.Parameters.AddWithValue("@requestId", requestId);
                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show("Inspection date updated and status updated successfully!",
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating maintenance request: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void reasonDecline_TextChanged(object sender, EventArgs e)
        {
            // Capture the reason for decline from the TextBox
            declineReason = reasonDecline.Text; // Assuming you have a TextBox named reasonDecline
        }

        private void requestIdForm_TextChanged(object sender, EventArgs e)
        {
            // This can be left empty since we're loading data on form load
        }

        private void tenantNameForm_TextChanged(object sender, EventArgs e)
        {
            // This can be left empty since we're loading data on form load
        }

        private void roomNumberForm_TextChanged(object sender, EventArgs e)
        {
            // This can be left empty since we're loading data on form load
        }

        private void maintenanceTypeForm_TextChanged(object sender, EventArgs e)
        {
            // This can be left empty since we're loading data on form load
        }

        private void descriptionForm_TextChanged(object sender, EventArgs e)
        {
            // This can be left empty since we're loading data on form load
        }

        private void dateSubmittedForm_TextChanged(object sender, EventArgs e)
        {
            // This can be left empty since we're loading data on form load
        }

        private void updateStatusForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This can be left empty since we're loading data on form load
        }

        private void statusForm_TextChanged(object sender, EventArgs e)
        {
            UpdatePanelVisibility();
        }

        private void dateInspection_ValueChanged(object sender, EventArgs e)
        {

        }

       

        

        private void sendEmailButton_Click(object sender, EventArgs e)
        {

        }
    }
}
