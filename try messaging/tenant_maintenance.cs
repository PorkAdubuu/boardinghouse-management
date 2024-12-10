using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class tenant_maintenance : Form
    {
        private int tenantId; // Store the tenant's ID
        private string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";

        public tenant_maintenance(int tenantId)
        {
            InitializeComponent();
            this.Load += new EventHandler(tenant_maintenance_Load);
            this.tenantId = tenantId;
        }

        private void tenant_maintenance_Load(object sender, EventArgs e)
        {
            LoadTenantRequests(tenantId); // Load tenant requests on form load
            LoadRequests();
        }

        private void typeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeCombo.SelectedItem != null)
            {
                string selectedType = typeCombo.SelectedItem.ToString();
                MessageBox.Show($"You selected: {selectedType}", "Maintenance Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void descriptionTextBox1_TextChanged(object sender, EventArgs e)
        {
            int maxLength = 500;
            if (descriptionTextBox1.Text.Length > maxLength)
            {
                descriptionTextBox1.Text = descriptionTextBox1.Text.Substring(0, maxLength);
                MessageBox.Show($"Description cannot exceed {maxLength} characters.", "Input Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Handle date change if needed
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // No changes needed here for handling images
        }

        private void uploadImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select an Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    InsertImageToRichTextBox(filePath);
                }
            }
        }

        private void removeImageButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void InsertImageToRichTextBox(string imagePath)
        {
            Image img = Image.FromFile(imagePath);
            Image stretchedImage = new Bitmap(richTextBox1.Width, richTextBox1.Height);
            using (Graphics g = Graphics.FromImage(stretchedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, 0, 0, stretchedImage.Width, stretchedImage.Height);
            }
            richTextBox1.Clear();
            using (MemoryStream ms = new MemoryStream())
            {
                stretchedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                Clipboard.SetImage(stretchedImage);
                richTextBox1.Paste();
            }
            img.Dispose();
            stretchedImage.Dispose();
        }

        private async void submitRequestButton_Click(object sender, EventArgs e)
        {
            string maintenanceType = typeCombo.SelectedItem?.ToString();
            string description = descriptionTextBox1.Text;
            DateTime requestDate = dateTimePicker1.Value; // Use for request_date
            byte[] imageData = ConvertImageToBytes();

            if (string.IsNullOrEmpty(maintenanceType) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Please select a maintenance type and provide a description.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlTransaction transaction = null;

                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    // Insert maintenance request
                    string query = "INSERT INTO maintenance_requests (tenant_id, maintenance_type, description, request_date, dateInspection, image_data, status) " +
                                   "VALUES (@tenantId, @maintenanceType, @description, @requestDate, @dateInspection, @imageData, @status)";
                    using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@tenantId", tenantId);
                        command.Parameters.AddWithValue("@maintenanceType", maintenanceType);
                        command.Parameters.AddWithValue("@description", description);
                        command.Parameters.AddWithValue("@requestDate", requestDate);
                        command.Parameters.AddWithValue("@dateInspection", DBNull.Value); // Set as NULL for now
                        command.Parameters.AddWithValue("@imageData", imageData ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@status", "Pending"); // Default status
                        command.ExecuteNonQuery();
                    }

                    // Insert notification into admin_notif table
                    string insertNotifQuery = @"
                    INSERT INTO admin_notif (reason, description, notif_type)
                    VALUES (NULL, @description, @notifType)";
                    using (MySqlCommand cmdInsertNotif = new MySqlCommand(insertNotifQuery, connection, transaction))
                    {
                        cmdInsertNotif.Parameters.AddWithValue("@description", "A maintenance request has been submitted by a tenant.");
                        cmdInsertNotif.Parameters.AddWithValue("@notifType", "Maintenance Request Submitted");
                        cmdInsertNotif.ExecuteNonQuery();
                    }

                    // Commit transaction
                    transaction.Commit();

                    MessageBox.Show("Maintenance request submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTenantRequests(tenantId); // Reload tenant requests after submission
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show($"Error submitting request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void resetFormButton_Click(object sender, EventArgs e)
        {
            typeCombo.SelectedIndex = -1;
            descriptionTextBox1.Clear();
            richTextBox1.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        private byte[] ConvertImageToBytes()
        {
            if (richTextBox1.TextLength > 0)
            {
                var data = Clipboard.GetDataObject();
                if (data.GetDataPresent(DataFormats.Bitmap))
                {
                    using (var image = (Image)data.GetData(DataFormats.Bitmap))
                    {
                        using (var ms = new MemoryStream())
                        {
                            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            return ms.ToArray();
                        }
                    }
                }
            }
            return null;
        }

        private void trackRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell click events if needed
        }

        private void LoadTenantRequests(int tenantId)
        {
            string query = "SELECT request_id, maintenance_type, description, request_date, dateInspection, status " +
                           "FROM maintenance_requests WHERE tenant_id = @tenantId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@tenantId", tenantId);

                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    trackRequests.DataSource = dataTable;

                    // Format the DataGridView
                    trackRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    trackRequests.AllowUserToResizeColumns = false;
                    trackRequests.AllowUserToResizeRows = false;
                    trackRequests.RowHeadersVisible = false;

                    // Optional: Rename or adjust columns for clarity
                    trackRequests.Columns["request_id"].HeaderText = "Request ID";
                    trackRequests.Columns["maintenance_type"].HeaderText = "Type";
                    trackRequests.Columns["description"].HeaderText = "Description";
                    trackRequests.Columns["request_date"].HeaderText = "Request Date";
                    trackRequests.Columns["dateInspection"].HeaderText = "Inspection Date"; // Fetch this from the database
                    trackRequests.Columns["status"].HeaderText = "Status";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading maintenance requests: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadRequests()
        {
            LoadTenantRequests(tenantId); // This function already loads tenant requests
        }
    }
}
