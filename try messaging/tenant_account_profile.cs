using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class tenant_account_profile : Form
    {
        private DatabaseConnection dbConnection;
        private int tenantId;
        public tenant_account_profile(int tenantId)
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
            this.tenantId = tenantId;

            addressLabel.BorderStyle = BorderStyle.None;
        }

        private void tenant_account_profile_Load(object sender, EventArgs e)
        {
            LoadTenantInformation();
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
                            // Combine first name and last name and set it in nameLabel
                            nameLabel.Text = $"{reader["lastname"].ToString().ToUpper()}, {reader["firstname"].ToString().ToUpper()}";
                            ageLabel.Text = reader["age"].ToString().ToUpper();
                            genderLabel.Text = reader["gender"].ToString().ToUpper();
                            addressLabel.Text = reader["address"].ToString().ToUpper();
                            emailLabel.Text = reader["email"].ToString();
                            phoneLabel.Text = reader["contact"].ToString().ToUpper();
                            phone1Label.Text = reader["emergency_contact1"].ToString().ToUpper();
                            phone2Label.Text = reader["emergency_contact2"].ToString().ToUpper();
                            name1Label.Text = reader["emergency_name1"].ToString().ToUpper();
                            name2Label.Text = reader["emergency_name2"].ToString().ToUpper();
                            roomLabel.Text = reader["roomnumber"].ToString().ToUpper();
                            moveinLabel.Text = Convert.ToDateTime(reader["movein_date"]).ToString("MM/dd/yyyy");
                            expirationLabel.Text = Convert.ToDateTime(reader["expiration_date"]).ToString("MM/dd/yyyy");

                            // Profile Picture: Check if the profile_picture column is not null
                            if (reader["profile_picture"] != DBNull.Value)
                            {
                                byte[] imageBytes = (byte[])reader["profile_picture"];
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    profilePic.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                // If no profile picture, you can set a default image
                                profilePic.Image = Properties.Resources.DefaultProfile; // Example: Default image
                            }
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


        private void profilePic_Click(object sender, EventArgs e)
        {

        }
    }
}
