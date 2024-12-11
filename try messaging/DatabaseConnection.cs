using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Wordprocessing;
using MySql.Data.MySqlClient; // Make sure to include this namespace
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace try_messaging
{
    public class DatabaseConnection
    {
        public string GetConnectionString()
        {
            return "Server=localhost;Database=boardinghouse_practice_db;Uid=root;Pwd=;";
        }

        public void InsertTenant(string lastname, string firstname, int age, int roomnumber, string email, string username, string password, string contact, string gender, string address, string emergency_name1, string emergency_name2, string emergency_contact1, string emergency_contact2, string wifi, string parking, DateTime movein_date, DateTime expiration_date, string houseName, DateTime birth_date)
        {
            string houseIdQuery = "SELECT house_id FROM boarding_houses WHERE house_name = @houseName";
            int houseId = 0;

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                connection.Open();

                // Step 1: Retrieve the house_id based on house_name
                using (MySqlCommand houseIdCommand = new MySqlCommand(houseIdQuery, connection))
                {
                    houseIdCommand.Parameters.AddWithValue("@houseName", houseName);

                    try
                    {
                        object result = houseIdCommand.ExecuteScalar();
                        if (result != null)
                        {
                            houseId = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("House name not found.");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving house ID: " + ex.Message);
                        return;
                    }
                }

                // Step 2: Insert tenant details including house_id
                string tenantInsertQuery = "INSERT INTO tenants_details (lastname, firstname, age, roomnumber, email, contact, gender, address, emergency_name1, emergency_name2, emergency_contact1, emergency_contact2, wifi, parking, movein_date, expiration_date, house_id, house_name, birth_date) " +
                                           "VALUES (@lastname, @firstname, @age, @roomnumber, @email, @contact, @gender, @address, @emergency_name1, @emergency_name2, @emergency_contact1, @emergency_contact2, @wifi, @parking, @movein_date, @expiration_date, @house_id, @houseName, @birth_date)";

                // Step 3: Insert into tenants_accounts
                string tenantAccountInsertQuery = "INSERT INTO tenants_accounts (tenid, username, password) VALUES (LAST_INSERT_ID(), @username, @password)"; // Use LAST_INSERT_ID() to get the last inserted tenid

                using (MySqlCommand tenantCommand = new MySqlCommand(tenantInsertQuery, connection))
                {
                    tenantCommand.Parameters.AddWithValue("@lastname", lastname);
                    tenantCommand.Parameters.AddWithValue("@firstname", firstname);
                    tenantCommand.Parameters.AddWithValue("@age", age);
                    tenantCommand.Parameters.AddWithValue("@roomnumber", roomnumber);
                    tenantCommand.Parameters.AddWithValue("@email", email);
                    tenantCommand.Parameters.AddWithValue("@contact", contact);
                    tenantCommand.Parameters.AddWithValue("@gender", gender);
                    tenantCommand.Parameters.AddWithValue("@address", address);
                    tenantCommand.Parameters.AddWithValue("@emergency_name1", emergency_name1);
                    tenantCommand.Parameters.AddWithValue("@emergency_name2", emergency_name2);
                    tenantCommand.Parameters.AddWithValue("@emergency_contact1", emergency_contact1);
                    tenantCommand.Parameters.AddWithValue("@emergency_contact2", emergency_contact2);
                    
                    tenantCommand.Parameters.AddWithValue("@wifi", wifi);
                    tenantCommand.Parameters.AddWithValue("@parking", parking);
                    tenantCommand.Parameters.AddWithValue("@movein_date", movein_date);
                    tenantCommand.Parameters.AddWithValue("@expiration_date", expiration_date);
                    tenantCommand.Parameters.AddWithValue("@house_id", houseId); // Include the house_id
                    tenantCommand.Parameters.AddWithValue("@houseName", houseName);
                    tenantCommand.Parameters.AddWithValue("@birth_date", birth_date);

                    try
                    {
                        tenantCommand.ExecuteNonQuery(); // Execute the first query to insert tenant
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inserting tenant: " + ex.Message);
                        return;
                    }
                }

                // Step 4: Insert into tenants_accounts table
                using (MySqlCommand tenantAccountCommand = new MySqlCommand(tenantAccountInsertQuery, connection))
                {
                    tenantAccountCommand.Parameters.AddWithValue("@username", username);
                    tenantAccountCommand.Parameters.AddWithValue("@password", password);

                    try
                    {
                        tenantAccountCommand.ExecuteNonQuery(); // Execute the second query to insert tenant account
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inserting tenant account: " + ex.Message);
                    }
                }
            }
        }


        public void InsertHouse(string house_name, string house_no, string location, int capacity)
        {
            string query = "INSERT INTO boarding_houses (house_name, house_no, location, capacity) VALUES (@house_name, @house_no, @location, @capacity);";

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@house_name", house_name);
                    command.Parameters.AddWithValue("@house_no", house_no);
                    command.Parameters.AddWithValue("@location", location);
                    command.Parameters.AddWithValue("@capacity", capacity);

                    try
                    {
                        command.ExecuteNonQuery(); // Execute the first query
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inserting data: " + ex.Message);
                        return;
                    }
                }
            }
        }

        public string GetAdminName(int adminId)
        {
            string adminName = string.Empty;
            string query = "SELECT CONCAT(lastname, ', ', firstname) FROM admin_details WHERE admin_details_id = @adminId";

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@adminId", adminId);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            adminName = result.ToString().ToUpper(); // Optionally, you can format it to uppercase.
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving admin name: " + ex.Message);
                    }
                }
            }

            return adminName;
        }



        public bool ValidateLogin(string username, string password)
        {
            string query = "SELECT COUNT(*) FROM tenants_accounts WHERE username = @username AND password = @password;";

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    // Execute the query and check if any record is found
                    int userCount = Convert.ToInt32(command.ExecuteScalar());
                    return userCount > 0; // Return true if user exists
                }
            }
        }
        public bool ValidateLoginAdmin(string username, string password)
        {
            string query = "SELECT COUNT(*) FROM admin_accounts WHERE username = @username AND password = @password;";

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    // Execute the query and check if any record is found
                    int userCount = Convert.ToInt32(command.ExecuteScalar());
                    return userCount > 0; // Return true if user exists
                }
            }
        }

        public string GetTenantEmail(int tenantId)
        {
            string email = string.Empty;
            string query = "SELECT email FROM tenants_details WHERE tenid = @tenid;";

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@tenid", tenantId);
                    try
                    {
                        email = command.ExecuteScalar() as string; // Retrieve the email
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error fetching email: " + ex.Message);
                    }
                }
            }

            return email; // Return the fetched email
        }
        public string GetAdminEmail(int adminId)
        {
            string email = string.Empty;
            string query = "SELECT email FROM admin_details WHERE admin_details_id = @adminId;";

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@adminId", adminId);
                    try
                    {
                        email = command.ExecuteScalar() as string; // Retrieve the email
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error fetching email: " + ex.Message);
                    }
                }
            }

            return email; // Return the fetched email
        }


        public int GetTenantId(string username, string password)
        {
            int tenantId = 0; // Default value if not found
            string query = "SELECT tenid FROM tenants_accounts WHERE username = @username AND password = @password;";

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        tenantId = Convert.ToInt32(result);
                    }
                }
            }
            return tenantId; // Return the tenant ID
        }
        public int GetAdminId(string username, string password)
        {
            int adminId = 0; // Default value if not found
            string query = "SELECT admin_id FROM admin_accounts WHERE username = @username AND password = @password;";

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    connection.Open();

                    object results = command.ExecuteScalar();
                    if (results != null)
                    {
                        adminId = Convert.ToInt32(results);
                    }
                }
            }
            return adminId; // Return the admin ID
        }

        public bool IsEmailOrContactExists(string email, string contact)
        {
            bool exists = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM tenants_details WHERE email = @Email OR contact = @Contact";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Contact", contact);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        exists = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking duplicates: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return exists;
        }

        public void InsertHouseWithPolicy(string houseName, string houseNo, string location, int capacity, byte[] policyFileData)
        {
            using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = @"INSERT INTO boarding_houses (house_name, house_no, location, capacity, policy)
                             VALUES (@houseName, @houseNo, @location, @capacity, @policy)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@houseName", houseName);
                    cmd.Parameters.AddWithValue("@houseNo", houseNo);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@capacity", capacity);
                    cmd.Parameters.AddWithValue("@policy", policyFileData); // Insert the policy as a BLOB

                    cmd.ExecuteNonQuery(); // Execute the query
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inserting house details with policy: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool IsBoardingHouseAvailable(string houseName)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT capacity, current_occupancy FROM boarding_houses WHERE house_name = @HouseName";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HouseName", houseName);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int capacity = reader.GetInt32("capacity");
                                int currentOccupancy = reader.GetInt32("current_occupancy");
                                return currentOccupancy < capacity;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking boarding house capacity: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false; // Default to unavailable if error occurs
        }

        public void UpdateCurrentOccupancy(string houseName)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "UPDATE boarding_houses SET current_occupancy = current_occupancy + 1 WHERE house_name = @HouseName";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HouseName", houseName);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating boarding house occupancy: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


    }
}
