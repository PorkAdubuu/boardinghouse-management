using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OxyPlot;

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
            roomText.BorderStyle = BorderStyle.None;
            moveinText.BorderStyle = BorderStyle.None;
            expirationText.BorderStyle = BorderStyle.None;
            name1Text.BorderStyle = BorderStyle.None;
            name2Text.BorderStyle = BorderStyle.None;
            emergencyNo1Text.BorderStyle = BorderStyle.None;
            emergencyNo2Text.BorderStyle = BorderStyle.None;



        }

        private void Personal_information_tenant_Load(object sender, EventArgs e)
        {
            LoadTenantInformation();
            updateBtn.Visible = false;
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
                            addressText.Text = reader["address"].ToString();
                            emailText.Text = reader["email"].ToString();
                            contactText.Text = reader["contact"].ToString();
                            emergencyNo1Text.Text = reader["emergency_contact1"].ToString();
                            emergencyNo2Text.Text = reader["emergency_contact2"].ToString();
                            name1Text.Text = reader["emergency_name1"].ToString();
                            name2Text.Text = reader["emergency_name2"].ToString();
                            roomText.Text = reader["roomnumber"].ToString();
                            moveinText.Text = Convert.ToDateTime(reader["movein_date"]).ToString("MM/dd/yyyy");
                            expirationText.Text = Convert.ToDateTime(reader["expiration_date"]).ToString("MM/dd/yyyy");
                            //add the profile 
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
                    string query = "UPDATE tenants_details SET emergency_contact1 = @emergencyContact1, emergency_contact2 = @emergencyContact2, emergency_name1 = @name1Textt, emergency_name2 = @name2Textt WHERE tenid = @tenantId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Set the parameters
                    cmd.Parameters.AddWithValue("@emergencyContact1", emergencyNo1Text.Text);
                    cmd.Parameters.AddWithValue("@emergencyContact2", emergencyNo2Text.Text);
                    cmd.Parameters.AddWithValue("@name1Textt", name1Text.Text);
                    cmd.Parameters.AddWithValue("@name2Textt", name2Text.Text);
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

                edit_Btn.Image = Properties.Resources.edit;
                name1Text.BorderStyle = BorderStyle.None;
                name2Text.BorderStyle = BorderStyle.None;
                emergencyNo1Text.BorderStyle = BorderStyle.None;
                emergencyNo2Text.BorderStyle = BorderStyle.None;

                name1Text.ReadOnly = true;
                name2Text.ReadOnly = true;
                emergencyNo1Text.ReadOnly = true;
                emergencyNo2Text.ReadOnly = true;

                updateBtn.Visible = false;

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

        private void name1Text_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void name1Text_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void name2Text_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void edit_Btn_Click(object sender, EventArgs e)
        {
            updateBtn.Visible = !updateBtn.Visible;
            name1Text.BorderStyle = BorderStyle.FixedSingle;
            name2Text.BorderStyle = BorderStyle.FixedSingle;
            emergencyNo1Text.BorderStyle = BorderStyle.FixedSingle;
            emergencyNo2Text.BorderStyle = BorderStyle.FixedSingle;
            edit_Btn.Image = Properties.Resources.done;

            name1Text.ReadOnly = false;
            name2Text.ReadOnly = false;
            emergencyNo1Text.ReadOnly = false;
            emergencyNo2Text.ReadOnly = false;
        }
    }
}
