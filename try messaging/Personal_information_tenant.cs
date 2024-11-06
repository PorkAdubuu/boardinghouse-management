using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class Personal_information_tenant : Form
    {
        private DatabaseConnection dbConnection;
        private int tenantId;
        private byte[] profilePictureData;       

        public Personal_information_tenant(int tenantId)
        {
            InitializeComponent();
            dbConnection = new DatabaseConnection();
            this.tenantId = tenantId;         

            // Text box settings 
            nameText.BorderStyle = BorderStyle.None;
            addressText.BorderStyle = BorderStyle.None;
            contactText.BorderStyle = BorderStyle.None;
            emailText.BorderStyle = BorderStyle.None;
            ageText.BorderStyle = BorderStyle.None;
            
        }

        private void Personal_information_tenant_Load(object sender, EventArgs e)
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
                            // Combine first name and last name and set it in nameText
                            nameText.Text = $"{reader["firstname"].ToString()} {reader["lastname"].ToString()}";
                            ageText.Text = reader["age"].ToString();
                            addressText.Text = $"Room {reader["roomnumber"].ToString()}"; // Assuming roomnumber is the address
                            emailText.Text = reader["email"].ToString();
                            contactText.Text = reader["contact"].ToString();
                            emergencyNo1Text.Text = reader["emergency_contact1"].ToString();
                            emergencyNo2Text.Text = reader["emergency_contact2"].ToString();
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

       

        private void updateBtn_Click_1(object sender, EventArgs e)
        {
            

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE tenants_details SET emergency_contact1 = @emergencyContact1, emergency_contact2 = @emergencyContact2 WHERE tenid = @tenantId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Set the parameters
                    cmd.Parameters.AddWithValue("@emergencyContact1", emergencyNo1Text.Text);
                    cmd.Parameters.AddWithValue("@emergencyContact2", emergencyNo2Text.Text);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    // Execute the update command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Emergency contacts updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No changes were made.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating emergency contacts: " + ex.Message);
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void nameText_TextChanged(object sender, EventArgs e)
        {

        }

        private void addressText_TextChanged(object sender, EventArgs e)
        {

        }

        private void contactText_TextChanged(object sender, EventArgs e)
        {

        }

        private void uploadBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog1.Title = "Select Profile Picture";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Display selected image in PictureBox for preview
                    profilePicture.Image = Image.FromFile(openFileDialog1.FileName);

                    // Load image file into byte array
                    using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            profilePictureData = ms.ToArray();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image file: " + ex.Message);
                    profilePictureData = null;
                }
            }
        }

        private void updatePictureBtn_Click(object sender, EventArgs e)
        {
            if (profilePictureData == null)
            {
                MessageBox.Show("Please upload a profile picture first.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE tenants_details SET profile_picture = @profilePicture WHERE tenid = @tenantId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@profilePicture", profilePictureData);
                    cmd.Parameters.AddWithValue("@tenantId", tenantId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile picture updated successfully.");

                        // Close the current tenant_dashboard form
                        foreach (Form openForm in Application.OpenForms)
                        {
                            if (openForm is tenant_dashboard)
                            {
                                openForm.Close();
                                break;
                            }
                        }

                        // Reopen tenant_dashboard and center it on the screen
                        tenant_dashboard newDashboard = new tenant_dashboard(tenantId); // Pass any required parameters like tenantId                      
                        newDashboard.Show();                       
                    }
                    else
                    {
                        MessageBox.Show("Failed to update profile picture.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating profile picture: " + ex.Message);
                }
            }
        }

        
        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            profilePicture.Image = null;
            profilePictureData = null;
        }

        private void profilePicture_Click(object sender, EventArgs e)
        {
            // Check if there is an image loaded in the PictureBox
            if (profilePicture.Image != null)
            {
                // Open the custom image preview dialog and pass the image
                ImagePreviewDialog previewDialog = new ImagePreviewDialog(profilePicture.Image);
                previewDialog.ShowDialog(); // Show the dialog as a modal (blocks interaction with the main form)
            }
            else
            {
                MessageBox.Show("No image selected to preview.");
            }
        }
    }
}
