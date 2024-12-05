using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class MaintenanceRequestForm : Form
    {
        private int requestId; // Store the request ID to fetch data
        private admin_maintenance adminForm; // Reference to the admin maintenance form

        public MaintenanceRequestForm(int requestId, admin_maintenance adminForm)
        {
            InitializeComponent();
            this.requestId = requestId; // Assign the request ID
            this.adminForm = adminForm; // Store the reference to the admin form
        }

        private void MaintenanceRequestForm_Load(object sender, EventArgs e)
        {
            LoadMaintenanceRequestDetails();
            PopulateStatusDropdown(); // Populate status dropdown with options
        }

        private void LoadMaintenanceRequestDetails()
        {
            try
            {
                string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"SELECT 
                                        mr.request_id, 
                                        CONCAT(td.lastname, ', ', td.firstname) AS tenant_name, 
                                        td.roomnumber, 
                                        mr.maintenance_type, 
                                        mr.description, 
                                        mr.request_date, 
                                        mr.status 
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
                        tenantNameForm.Text = reader["tenant_name"].ToString();
                        roomNumberForm.Text = reader["roomnumber"].ToString();
                        maintenanceTypeForm.Text = reader["maintenance_type"].ToString();
                        descriptionForm.Text = reader["description"].ToString();
                        dateSubmittedForm.Text = Convert.ToDateTime(reader["request_date"]).ToString("yyyy-MM-dd");

                        // Set the current status in the TextBox
                        statusForm.Text = reader["status"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading maintenance request details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateStatusDropdown()
        {
            // Clear existing items and populate dropdown with possible statuses
            updateStatusForm.Items.Clear();
            updateStatusForm.Items.AddRange(new string[] { "Done", "In Progress", "Pending" });
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

        private void statusForm_TextChanged(object sender, EventArgs e)
        {
            // This can be left empty since we're loading data on form load
        }

        private void updateStatusForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This can be left empty since we're allowing selection
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            // Check if an item is selected in the updateStatusForm ComboBox
            if (updateStatusForm.SelectedItem == null)
            {
                MessageBox.Show("Please select a status before saving.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string connectionString = "server=localhost;user=root;database=boardinghouse_practice_db;port=3306;password=;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = @"UPDATE maintenance_requests 
                                     SET status = @status 
                                     WHERE request_id = @requestId";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@status", updateStatusForm.SelectedItem.ToString()); // Get the selected status from the ComboBox
                    command.Parameters.AddWithValue("@requestId", requestId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Maintenance request status updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMaintenanceRequestDetails(); // Refresh the details to show updated status

                // Notify the admin_maintenance form to refresh its DataGridView
                adminForm.RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating maintenance request: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            // Close the current MaintenanceRequestForm
            this.Close();
        }
    }
}
