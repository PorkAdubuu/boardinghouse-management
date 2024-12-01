using MySql.Data.MySqlClient;  // For MySQL database connection
using System;
using System.Data;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class Payment : Form
    {
        // Connection string to your database
        private string connectionString = "server=localhost;database=boardinghouse_practice_db;uid=root;pwd=your_password;";

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Remove this if not using custom painting
        }

        public Payment()
        {
            this.CenterToScreen();
            InitializeComponent();
            LoadPaymentData(); 
        }

        // Method to load payment data into the DataGridView
        private void LoadPaymentData()
        {
            // SQL query to fetch payment details
            string query = @"SELECT 
                                td.tenid, 
                                CONCAT(td.firstname, ' ', td.lastname) AS full_name, 
                                td.roomnumber, 
                                tp.amount_due, 
                                tp.balance, 
                                tp.payment_status, 
                                tp.last_payment_date 
                            FROM tenants_details td 
                            LEFT JOIN tenant_payments tp ON td.tenid = tp.tenid";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    tableforpayment.DataSource = dataTable; // Bind data to DataGridView

                    // Add "Edit Bills" link column if it doesn't exist
                    if (!tableforpayment.Columns.Contains("Edit Bills"))
                    {
                        DataGridViewLinkColumn editColumn = new DataGridViewLinkColumn
                        {
                            Name = "Edit Bills",
                            Text = "Edit Bills",
                            UseColumnTextForLinkValue = true
                        };
                        tableforpayment.Columns.Add(editColumn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        // Event handler for cell clicks in the DataGridView
        private void tableforpayment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the "Edit Bills" column
            if (tableforpayment.Columns[e.ColumnIndex].Name == "Edit Bills")
            {
                // Get the Tenant ID of the selected row
                int tenantId = Convert.ToInt32(tableforpayment.Rows[e.RowIndex].Cells["tenid"].Value);

                // Open the Edit Bills form
                EditBillsForm editForm = new EditBillsForm(tenantId);
                editForm.ShowDialog();

                // Reload the payment data after editing
                LoadPaymentData();
            }
        }

        // Dashboard button click event (optional, based on your design)
        private void dashboard_Btn_Click(object sender, EventArgs e)
        {
            // Logic to navigate back to the dashboard or main menu
            this.Close(); // Example: Close the Payment form
        }
    }
}
