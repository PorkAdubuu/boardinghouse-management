using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class adminChangeProfile : Form
    {
        private int adminId;
        private DatabaseConnection dbConnection;
        private byte[] profile_pictureData;
        public adminChangeProfile(int adminId)
        {
            InitializeComponent();
            this.CenterToParent();
            dbConnection = new DatabaseConnection();
            this.adminId = adminId;
        }

        private void adminChangeProfile_Load(object sender, EventArgs e)
        {
            LoadAdminProfilePicture(adminId);
        }
        private void LoadAdminProfilePicture(int adminId)
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT profile_picture FROM admin_details WHERE admin_details_id = @adminId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@adminId", adminId);

                    byte[] imageData = cmd.ExecuteScalar() as byte[];

                    if (imageData != null && imageData.Length > 0)
                    {
                        // Convert byte array to image and display in PictureBox
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            profile_picture.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        // If no image found, use default image
                        profile_picture.Image = Properties.Resources.DefaultProfile;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading profile picture: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog1.Title = "Select Profile Picture";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Display selected image in PictureBox for preview
                    profile_picture.Image = Image.FromFile(openFileDialog1.FileName);

                    // Load image file into byte array
                    using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            profile_pictureData = ms.ToArray();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image file: " + ex.Message);
                    profile_pictureData = null;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (profile_pictureData == null)
            {
                MessageBox.Show("Please upload a profile picture first.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE admin_details SET profile_picture = @profilePicture WHERE admin_details_id = @adminId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@profilePicture", profile_pictureData);
                    cmd.Parameters.AddWithValue("@adminId", adminId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Profile picture updated successfully.");

                        // Close the current tenant_dashboard form
                        foreach (Form openForm in Application.OpenForms)
                        {
                            if (openForm is admin_dashboard)
                            {
                                openForm.Close();
                                break;
                            }
                        }

                        // Reopen tenant_dashboard and center it on the screen
                        admin_dashboard newDashboard = new admin_dashboard(adminId); // Pass any required parameters like tenantId                      
                        newDashboard.Show();
                        this.Close();
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
    }
}
