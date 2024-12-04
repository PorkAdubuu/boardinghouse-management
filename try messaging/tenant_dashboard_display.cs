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
using System.Drawing.Drawing2D;
using System.IO;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;



namespace try_messaging
{
    public partial class tenant_dashboard_display : Form
    {
        private DatabaseConnection dbConnection;
        private int tenantId;
        public tenant_dashboard_display(int tenantId)
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
            this.tenantId = tenantId;
            LoadAnnouncements();


            this.BackColor = ColorTranslator.FromHtml("#f7f7f7");
            this.ActiveControl = null;
            bulletinForm bulletinForm = new bulletinForm();
            LoadFormInPanel(bulletinForm);
            LoadTenantInformation();

            





        }
        

        private void tenant_dashboard_display_Load(object sender, EventArgs e)
        {
            moveinText.BorderStyle = BorderStyle.None;
            expirationText.BorderStyle = BorderStyle.None;

            LoadHOuseInformation();
            LoadTenantBills();
            LoadTenantLastTransactions();

            LoadTotalPaid();
            LoadAmountpaid();



        }
        private void LoadTenantInformation()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM tenants_details WHERE tenid = @tenantId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            moveinText.Text = Convert.ToDateTime(reader["movein_date"]).ToString("MM/dd/yyyy");
                            expirationText.Text = Convert.ToDateTime(reader["expiration_date"]).ToString("MM/dd/yyyy");

                            // Load the house image from the boarding_houses table based on the house_name
                            LoadBoardingHouseImage(reader["house_name"].ToString());
                        }
                        else
                        {
                            MessageBox.Show("Tenant information not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tenant information: " + ex.Message);
                }
            }
        }

        private void LoadHOuseInformation()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    // Get the house_name of the tenant from tenants_details
                    string query = @"
                SELECT bh.house_name, bh.location 
                FROM boarding_houses bh
                INNER JOIN tenants_details td ON td.house_name = bh.house_name
                WHERE td.tenid = @tenantId";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the house name and location to the corresponding controls
                            houseNameText.Text = reader["house_name"].ToString().ToUpper();
                            
                        }
                        else
                        {
                            MessageBox.Show("House information not found for the tenant.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading house information: " + ex.Message);
                }
            }
        }

        private void LoadBoardingHouseImage(string houseName)
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT house_image FROM boarding_houses WHERE house_name = @houseName";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@houseName", houseName);

                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        byte[] imageBytes = (byte[])result;
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            boarding_image.Image = Image.FromStream(ms); // Set the image in the PictureBox
                        }
                    }
                    else
                    {
                        MessageBox.Show("House image not found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading house image: " + ex.Message);
                }
            }
        }
        private void LoadFormInPanel(Form childForm)
        {
            panel1.Controls.Clear(); // Remove existing controls

            childForm.TopLevel = false; // Treat the child form as a control
            childForm.FormBorderStyle = FormBorderStyle.None; // Remove borders
            childForm.Dock = DockStyle.Fill; // No automatic docking

            // Set the width of the child form to match the panel's width
            childForm.Width = panel1.ClientSize.Width;

            // Add the child form and show it
            panel1.Controls.Add(childForm);
            childForm.Show();
        }

        private void LoadAnnouncements()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT message, date_time FROM announcements ORDER BY date_time DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        announcementLog.Clear(); // Clear existing content

                        while (reader.Read())
                        {
                            string dateTime = Convert.ToDateTime(reader["date_time"]).ToString("yyyy-MM-dd HH:mm:ss");
                            string message = reader["message"].ToString();

                            // Automated announcement format
                            string formattedAnnouncement = $"[{dateTime}]\nGood day, everyone!\n\nThis is your admin, and I would like to share an important announcement:\n\n{message}\n\nThank you for your attention!\n\n";

                            // Append formatted announcement with color
                            announcementLog.SelectionStart = announcementLog.TextLength;
                            announcementLog.SelectionColor = Color.Blue; // Date-Time Color
                            announcementLog.AppendText($"[{dateTime}]\n");

                            announcementLog.SelectionStart = announcementLog.TextLength;
                            announcementLog.SelectionColor = Color.Black; // Message Color
                            announcementLog.AppendText("Good day, everyone!\n\nThis is your admin, and I would like to share an important announcement:\n\n");

                            announcementLog.SelectionStart = announcementLog.TextLength;
                            announcementLog.SelectionColor = Color.Green; // Highlight Message
                            announcementLog.AppendText($"{message}\n\n");

                            announcementLog.SelectionStart = announcementLog.TextLength;
                            announcementLog.SelectionColor = Color.Gray; // Closure Color
                            announcementLog.AppendText("Thank you for your attention!\n\n");

                            // Reset color
                            announcementLog.SelectionColor = announcementLog.ForeColor;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading announcements: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    SELECT total_bill, amount_paid, due_date
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
                            decimal totalBillFromDb = reader["total_bill"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_bill"]);
                            decimal amountPaid = reader["amount_paid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["amount_paid"]);
                            decimal remainingBalance = totalBillFromDb - amountPaid; // Calculate the remaining balance
                            totalBill.Text = remainingBalance.ToString("N2");
                            amount.Text = Convert.ToDecimal(reader["amount_paid"]).ToString("N2");
                            dueDate.Text = Convert.ToDateTime(reader["due_date"]).ToString("MM/dd/yyyy");

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
        private void LoadAmountpaid()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT amount_paid
                    FROM tenant_transaction_table
                    WHERE tenant_id = @tenantId
                    ORDER BY date DESC";  // Fetch the latest unpaid bill for the tenant

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);  // Pass only tenant_id

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            amount.Text = Convert.ToDecimal(reader["amount_paid"]).ToString("N2");

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
        private void LoadTotalPaid()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT amount_paid
                    FROM billing_table
                    WHERE tenant_id = @tenantId AND status IN ('Paid')
                    ORDER BY billing_id DESC LIMIT 1";  // Fetch the latest unpaid bill for the tenant

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);  // Pass only tenant_id

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Set the bill values for the labels, formatted with thousand separators and 2 decimal points
                            
                            decimal amountPaid = reader["amount_paid"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["amount_paid"]);
                            amount.Text = Convert.ToDecimal(reader["amount_paid"]).ToString("N2");

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

        private void LoadTenantLastTransactions()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"
                    SELECT total_bill, date, status
                    FROM tenant_transaction_table
                    WHERE tenant_id = @tenantId
                    ORDER BY date DESC
                    LIMIT 1";  // Fetch only the latest transaction for the current tenant



                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);  // Use the tenantId of the logged-in tenant

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())  // Check if any data is returned
                        {
                            // Set the values for the labels
                            
                            datePaid.Text = Convert.ToDateTime(reader["date"]).ToString("MM/dd/yyyy"); // Correct column name
                            paymentStatus.Text = reader["status"].ToString();
                        }
                        else
                        {
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading transactions: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
