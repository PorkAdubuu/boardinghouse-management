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

        public void InsertTenant(string lastname, string firstname, int age, int roomnumber, string email, string username, string password, string contact, string gender, string address, string emergency_name1, string emergency_name2, string emergency_contact1, string emergency_contact2, DateTime movein_date, DateTime expiration_date)
{
    // Update your query to include the new columns
    string query = "INSERT INTO tenants_details (lastname, firstname, age, roomnumber, email, contact, gender, address, emergency_name1, emergency_name2, emergency_contact1, emergency_contact2, movein_date, expiration_date) VALUES (@lastname, @firstname, @age, @roomnumber, @email, @contact, @gender, @address, @emergency_name1, @emergency_name2, @emergency_contact1, @emergency_contact2, @movein_date, @expiration_date);";
    string query2 = "INSERT INTO tenants_accounts (tenid, username, password) VALUES (LAST_INSERT_ID(), @username, @password);"; // Use LAST_INSERT_ID() to get the last inserted tenid

    using (MySqlConnection connection = new MySqlConnection(GetConnectionString()))
    {
        connection.Open();

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            // Add parameters to prevent SQL injection
            command.Parameters.AddWithValue("@lastname", lastname);
            command.Parameters.AddWithValue("@firstname", firstname);
            command.Parameters.AddWithValue("@age", age);
            command.Parameters.AddWithValue("@roomnumber", roomnumber);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@contact", contact); // Add contact parameter
            command.Parameters.AddWithValue("@gender", gender); // Add gender parameter
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@emergency_name1", emergency_name1);
            command.Parameters.AddWithValue("@emergency_name2", emergency_name2);
            command.Parameters.AddWithValue("@emergency_contact1", emergency_contact1);
            command.Parameters.AddWithValue("@emergency_contact2", emergency_contact2);
            command.Parameters.AddWithValue("@movein_date", movein_date);
            command.Parameters.AddWithValue("@expiration_date", expiration_date);

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

        using (MySqlCommand command2 = new MySqlCommand(query2, connection))
        {
            command2.Parameters.AddWithValue("@username", username);
            command2.Parameters.AddWithValue("@password", password);

            try
            {
                command2.ExecuteNonQuery(); // Execute the second query
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data into accounts: " + ex.Message);
            }
        }
    }
}

        public string GetAdminName(int adminId)
        {
            string adminName = string.Empty;
            string query = "SELECT name FROM admin_accounts WHERE admin_id = @adminId";

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
                            adminName = result.ToString();
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
    }
}
