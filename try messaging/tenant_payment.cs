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

    public partial class tenant_payment : Form
    {
        private int selectedBillingId;
        private int tenantId;  // Tenant ID (logged in tenant's ID)
        private DatabaseConnection dbConnection;

        public tenant_payment(int tenantId)
        {
            InitializeComponent();
            this.tenantId = tenantId;
            
            dbConnection = new DatabaseConnection();
        }

        private void tenant_payment_Load(object sender, EventArgs e)
        {
            LoadTenantBills();
            LoadTenantTransactions();
            LoadTenantBillsLog();

            if (billsTable.Rows.Count > 0)
            {
                billsTable.Rows[0].Selected = true;  // Select the first row
                selectedBillingId = Convert.ToInt32(billsTable.Rows[0].Cells["billing_id"].Value);  // Get the billing_id from the first row

                // Optionally, display the total bill of the first row
                decimal totalBill = Convert.ToDecimal(billsTable.Rows[0].Cells["Total Bill"].Value);
                // displayBillDetailsLabel.Text = $"Selected Bill: {totalBill:C}";
            }
        }

        
        private void LoadTenantTransactions()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT 
                    total_bill AS 'Total Bill',
                    date AS 'Date',
                    status AS 'Status',
                    reference_number AS 'Reference Number', 
                    amount_paid AS 'Amount Paid'
                    FROM tenant_transaction_table
                    WHERE tenant_id = @tenantId
                    ORDER BY date DESC";  // Fetch transactions for the current tenant

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);  // Use the tenantId of the logged-in tenant

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable transactionTable = new DataTable();
                    adapter.Fill(transactionTable);

                    paymentLog.DataSource = transactionTable;

                    paymentLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    paymentLog.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    paymentLog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    paymentLog.AllowUserToAddRows = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading transactions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadTenantBillsLog()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT 
                    billing_id,  -- Include billing_id in the SELECT statement
                    total_bill AS 'Total Bill',
                    issue_date AS 'Date',
                    start_date AS 'From',
                    end_date AS 'To',
                    due_date AS 'Due Date',
                    status AS 'Status',                     
                    amount_paid AS 'Amount Paid'
                    FROM billing_table
                    WHERE tenant_id = @tenantId
                    AND status IN ('No payment', 'Pending', 'Declined')  -- Filter by specific statuses
                    ORDER BY issue_date DESC";  // Fetch transactions for the current tenant

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);  // Use the tenantId of the logged-in tenant

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable transactionTable = new DataTable();
                    adapter.Fill(transactionTable);

                    billsTable.DataSource = transactionTable;

                    // Hide the billing_id column from the DataGridView
                    billsTable.Columns["billing_id"].Visible = false;

                    // Adjust table appearance
                    billsTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    billsTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    billsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    billsTable.AllowUserToAddRows = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading transactions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                SELECT wifi_bill, parking_bill, water_bill, electric_bill, rent_bill, remaining_balance, amount_paid, start_date, end_date, issue_date, due_date, status
                FROM billing_table
                WHERE tenant_id = @tenantId AND status IN ('No payment', 'Pending', 'Declined')
                ORDER BY billing_id DESC";  // Fetch all unpaid bills for the tenant

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);  // Pass only tenant_id

                    decimal totalBillSum = 0;
                    decimal electricBillSum = 0;
                    decimal waterBillSum = 0;
                    decimal wifiBillSum = 0;
                    decimal parkingBillSum = 0;
                    decimal rentBillSum = 0;

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Sum up the bill values for each category
                            totalBillSum += reader["remaining_balance"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["remaining_balance"]);
                            electricBillSum += reader["electric_bill"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["electric_bill"]);
                            waterBillSum += reader["water_bill"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["water_bill"]);
                            wifiBillSum += reader["wifi_bill"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["wifi_bill"]);
                            parkingBillSum += reader["parking_bill"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["parking_bill"]);
                            rentBillSum += reader["rent_bill"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["rent_bill"]);
                        }

                        // Display the total bill sums in the text boxes
                        electricBill.Text = electricBillSum.ToString("N2");
                        waterBill.Text = waterBillSum.ToString("N2");
                        wifiBill.Text = wifiBillSum.ToString("N2");
                        parkingBill.Text = parkingBillSum.ToString("N2");
                        rentBill.Text = rentBillSum.ToString("N2");
                        totalBill.Text = totalBillSum.ToString("N2");

                        // Display the most recent billing date details
                        if (reader.HasRows)
                        {
                            // Go back to the first row to get the most recent billing info for dates
                            reader.Close();
                            cmd.CommandText = @"
                        SELECT start_date, end_date, issue_date, due_date 
                        FROM billing_table
                        WHERE tenant_id = @tenantId AND status IN ('No payment', 'Pending', 'Declined')
                        ORDER BY billing_id DESC LIMIT 1";  // Fetch the most recent billing date info

                            using (MySqlDataReader dateReader = cmd.ExecuteReader())
                            {
                                if (dateReader.Read())
                                {
                                    fromDate.Text = Convert.ToDateTime(dateReader["start_date"]).ToString("MM/dd/yyyy");
                                    endDate.Text = Convert.ToDateTime(dateReader["end_date"]).ToString("MM/dd/yyyy");
                                    dueDate.Text = Convert.ToDateTime(dateReader["due_date"]).ToString("MM/dd/yyyy");
                                    issueDate.Text = Convert.ToDateTime(dateReader["issue_date"]).ToString("MM/dd/yyyy");
                                }
                            }
                        }
                    }

                    // Enable or disable the "Pay Now" button based on the bill status
                    if (totalBillSum == 0)
                    {
                        paynow_Btn.Enabled = false;
                        paynow_Btn.BackColor = Color.Gray;
                    }
                    else
                    {
                        paynow_Btn.Enabled = true;
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tenant bills: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }




        private void paynow_Btn_Click(object sender, EventArgs e)
        {
           

            // Pass the selected billing_id (and any other needed details) to tenant_paymentMODE
            tenant_paymentMODE tenant_PaymentMODE = new tenant_paymentMODE(tenantId, selectedBillingId);
            tenant_PaymentMODE.tenant_paymentMODEClosed += tenant_paymentMODDE_Closed;
            tenant_PaymentMODE.Show();
        }

        private void refresh_Btn_Click(object sender, EventArgs e)
        {
            LoadTenantBills();
            LoadTenantTransactions();
            LoadTenantBillsLog();
        }

        private void tenant_paymentMODDE_Closed()
        {
            LoadTenantBills();
            LoadTenantTransactions();
            LoadTenantBillsLog();
        }

        private void paymentLog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        private void billsTable_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Make sure a valid cell was clicked (not the header row)
            if (e.RowIndex >= 0)
            {
                // Get the billing_id from the clicked row (assuming the billing_id is the first column)
                selectedBillingId = Convert.ToInt32(billsTable.Rows[e.RowIndex].Cells["billing_id"].Value);

                // Highlight the entire row
                foreach (DataGridViewRow row in billsTable.Rows)
                {
                    row.Selected = false;  // Unselect all rows
                }
                billsTable.Rows[e.RowIndex].Selected = true;  // Select the clicked row

                // Optionally, you can also display other details from the clicked row
                decimal totalBill = Convert.ToDecimal(billsTable.Rows[e.RowIndex].Cells["Total Bill"].Value);

                // For example, you could display the total bill in a label
                // displayBillDetailsLabel.Text = $"Selected Bill: {totalBill:C}";
            }
        }
    }
}
