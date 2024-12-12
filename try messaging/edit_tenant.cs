using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class edit_tenant : Form
    {
        private DatabaseConnection dbConnection;
        private int tenantId;  // Store tenant ID
        public edit_tenant(int tenantId)
        {
            InitializeComponent();
            this.CenterToParent();
            dbConnection = new DatabaseConnection();
            this.tenantId = tenantId;
        }

        private void edit_tenant_Load(object sender, EventArgs e)
        {
            LoadTenantDetails();
            LoadTenantCredentials();
            LoadBoardingHouses();
        }

        private void LoadTenantDetails()
        {
            // Here you can write code to load tenant data based on tenantId
            string query = "SELECT * FROM tenants_details WHERE tenid = @tenantId";
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate your form fields with the tenant details
                        fnameText.Text = reader["firstname"].ToString();
                        lnameText.Text = reader["lastname"].ToString();
                        roomText.Text = reader["roomnumber"].ToString();
                        ageText.Text = reader["age"].ToString();
                        genderCombo.Text = reader["gender"].ToString();
                        contactText.Text = reader["contact"].ToString();
                        emailText.Text = reader["email"].ToString();
                        addText.Text = reader["address"].ToString();
                        houseCombo.Text = reader["house_name"].ToString();
                        paxText.Text = reader["pax_number"].ToString();


                        if (reader["birth_date"] != DBNull.Value)
                        {
                            DateTime birthDate = Convert.ToDateTime(reader["birth_date"]);
                            birthDatePicker.Value = birthDate;
                        }
                        else
                        {
                            
                            MessageBox.Show("Birth date is not available.");
                        }
                        // Set the move-in date
                        if (reader["movein_date"] != DBNull.Value)
                        {
                            movein_datapicker.Value = Convert.ToDateTime(reader["movein_date"]);
                        }
                        else
                        {
                            MessageBox.Show("Move-in date is not available.");
                        }

                        // Set the expiration date
                        if (reader["expiration_date"] != DBNull.Value)
                        {
                            expiration_datapicker.Value = Convert.ToDateTime(reader["expiration_date"]);
                        }
                        else
                        {
                            MessageBox.Show("Expiration date is not available.");
                        }

                        // WiFi Checkbox
                        if (reader["wifi"] != DBNull.Value && reader["wifi"].ToString().ToUpper() == "YES")
                        {
                            amenities1.Checked = true;
                        }
                        else
                        {
                            amenities1.Checked = false;
                        }

                        // Parking Checkbox
                        if (reader["parking"] != DBNull.Value && reader["parking"].ToString().ToUpper() == "YES")
                        {
                            amenities2.Checked = true;
                        }
                        else
                        {
                            amenities2.Checked = false;
                        }



                    }
                    else
                    {
                        MessageBox.Show("Tenant not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading tenant details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadTenantCredentials()
        {
            // Here you can write code to load tenant data based on tenantId
            string query = "SELECT * FROM tenants_accounts WHERE tenid = @tenantId";
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate your form fields with the tenant details
                        usernameText.Text = reader["username"].ToString();
                        passwordText.Text = reader["password"].ToString();                     
                    }
                    else
                    {
                        MessageBox.Show("Tenant not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading tenant details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadBoardingHouses()
        {
            // Query to get all available boarding houses
            string query = "SELECT DISTINCT house_name FROM boarding_houses";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            houseCombo.Items.Clear(); // Clear existing items

                            while (reader.Read())
                            {
                                houseCombo.Items.Add(reader["house_name"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading boarding houses: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void update_Btn_Click(object sender, EventArgs e)
        {

            // Show the password prompt form
            using (PasswordPromptForm passwordPrompt = new PasswordPromptForm())
            {
                if (passwordPrompt.ShowDialog() == DialogResult.OK)
                {
                    // Get the entered password
                    string adminPassword = passwordPrompt.AdminPassword;

                    // Verify the admin password
                    if (VerifyAdminPassword(adminPassword))
                    {
                        // Proceed with the update logic
                        UpdateTenantDetails();
                    }
                    else
                    {
                        MessageBox.Show("Invalid admin password. Update canceled.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            
        }

        private void UpdateTenantDetails()
        {
            string tenantDetailsQuery = @"UPDATE tenants_details 
                                  SET firstname = @firstname,
                                      lastname = @lastname,
                                      roomnumber = @roomnumber,
                                      age = @age,
                                      gender = @gender,
                                      contact = @contact,
                                      email = @email,
                                      address = @address,
                                      house_name = @houseName,
                                      pax_number = @paxNumber,
                                      birth_date = @birthDate,
                                      movein_date = @moveInDate,
                                      expiration_date = @expirationDate,
                                      wifi = @wifi,
                                      parking = @parking
                                  WHERE tenid = @tenantId";

            string accountQuery = @"UPDATE tenants_accounts
                            SET username = @username,
                                password = @password
                            WHERE tenid = @tenantId";

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    // Update tenant details
                    MySqlCommand tenantCmd = new MySqlCommand(tenantDetailsQuery, conn);
                    tenantCmd.Parameters.AddWithValue("@firstname", fnameText.Text.Trim());
                    tenantCmd.Parameters.AddWithValue("@lastname", lnameText.Text.Trim());
                    tenantCmd.Parameters.AddWithValue("@roomnumber", roomText.Text.Trim());
                    tenantCmd.Parameters.AddWithValue("@age", ageText.Text.Trim());
                    tenantCmd.Parameters.AddWithValue("@gender", genderCombo.Text.Trim());
                    tenantCmd.Parameters.AddWithValue("@contact", contactText.Text.Trim());
                    tenantCmd.Parameters.AddWithValue("@email", emailText.Text.Trim());
                    tenantCmd.Parameters.AddWithValue("@address", addText.Text.Trim());
                    tenantCmd.Parameters.AddWithValue("@houseName", houseCombo.Text.Trim());
                    tenantCmd.Parameters.AddWithValue("@paxNumber", paxText.Text.Trim());
                    tenantCmd.Parameters.AddWithValue("@birthDate", birthDatePicker.Value.ToString("yyyy-MM-dd"));
                    tenantCmd.Parameters.AddWithValue("@moveInDate", movein_datapicker.Value.ToString("yyyy-MM-dd"));
                    tenantCmd.Parameters.AddWithValue("@expirationDate", expiration_datapicker.Value.ToString("yyyy-MM-dd"));
                    tenantCmd.Parameters.AddWithValue("@wifi", amenities1.Checked ? "YES" : "NO");
                    tenantCmd.Parameters.AddWithValue("@parking", amenities2.Checked ? "YES" : "NO");
                    tenantCmd.Parameters.AddWithValue("@tenantId", tenantId);

                    int tenantRowsAffected = tenantCmd.ExecuteNonQuery();

                    // Update tenant account
                    MySqlCommand accountCmd = new MySqlCommand(accountQuery, conn);
                    accountCmd.Parameters.AddWithValue("@username", usernameText.Text.Trim());
                    accountCmd.Parameters.AddWithValue("@password", passwordText.Text.Trim());
                    accountCmd.Parameters.AddWithValue("@tenantId", tenantId);

                    int accountRowsAffected = accountCmd.ExecuteNonQuery();

                    // Check if any rows were affected
                    if (tenantRowsAffected > 0 || accountRowsAffected > 0)
                    {
                        MessageBox.Show("Tenant details and account updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No changes were made.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while updating tenant details or account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool VerifyAdminPassword(string password)
        {
            string query = "SELECT COUNT(*) FROM admin_accounts WHERE password = @password"; // Adjust if username is needed
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@password", password); // Hash the password if necessary

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    // Return true if a match is found
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error verifying admin password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

    }
}

