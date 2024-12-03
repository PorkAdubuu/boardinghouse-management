﻿using MySql.Data.MySqlClient;
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
        }
        private void LoadTenantTransactions()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT total_bill, date, status, reference_number
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


        private void LoadTenantBills()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT aircon_bill, wifi_bill, parking_bill, water_bill, electric_bill, rent_bill, total_bill, start_date, end_date
                    FROM billing_table
                    WHERE tenant_id = @tenantId AND status IN ('No payment', 'Pending', 'Declined')
                    ORDER BY billing_id DESC LIMIT 1";  // Fetch the latest unpaid bill for the tenant

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);  // Pass only tenant_id

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the bill values for the labels, formatted with thousand separators and 2 decimal points
                            electricBill.Text = Convert.ToDecimal(reader["electric_bill"]).ToString("N2");
                            waterBill.Text = Convert.ToDecimal(reader["water_bill"]).ToString("N2");
                            wifiBill.Text = Convert.ToDecimal(reader["wifi_bill"]).ToString("N2");
                            parkingBill.Text = Convert.ToDecimal(reader["parking_bill"]).ToString("N2");
                            rentBill.Text = Convert.ToDecimal(reader["rent_bill"]).ToString("N2");
                            totalBill.Text = Convert.ToDecimal(reader["total_bill"]).ToString("N2");

                            fromDate.Text = Convert.ToDateTime(reader["start_date"]).ToString("MM/dd/yyyy");
                            endDate.Text = Convert.ToDateTime(reader["end_date"]).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            

                            
                        }
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
            tenant_paymentMODE tenant_PaymentMODE = new tenant_paymentMODE(tenantId);
            tenant_PaymentMODE.Show();
        }

        private void refresh_Btn_Click(object sender, EventArgs e)
        {
            LoadTenantBills();
            LoadTenantTransactions();
        }
    }
}