﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class admin_account_profile : Form
    {
        private DatabaseConnection dbConnection;
        private int adminId;
        private string verificationCode; // To store the verification code

        public admin_account_profile(string verificationCode, int adminId)
        {
            InitializeComponent();
            this.adminId = adminId;
            this.verificationCode = verificationCode;
            dbConnection = new DatabaseConnection();

            addressLabel.BorderStyle = BorderStyle.None;

        }

        private void admin_account_profile_Load(object sender, EventArgs e)
        {
            LoadAdminInformation();
            LoadAdminverificationcode();

        }
        private void LoadAdminverificationcode()
        {
            // Initialize the VerificationCode form with the adminId and a reference to tenant_profile
            adminverificationCode adminverificationCode = new adminverificationCode(verificationCode, adminId, this);

            adminverificationCode.TopLevel = false;
            adminverificationCode.FormBorderStyle = FormBorderStyle.None;
            adminverificationCode.Dock = DockStyle.Fill;

            // Clear any previous controls in the changePassPanel
            panel1.Controls.Clear();

            // Add the form to the panel and display it
            panel1.Controls.Add(adminverificationCode);
            adminverificationCode.Show();
        }
        
        private void LoadAdminInformation()
        {
            using (MySqlConnection conn = new MySqlConnection(dbConnection.GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM admin_details WHERE admin_details_id = @adminId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@adminId", adminId);

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
                            phoneLabel.Text = reader["phone_number"].ToString().ToUpper();

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
                            MessageBox.Show("Admin information not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Admin information: " + ex.Message);
                }
            }
        }

        private void profilePic_Click(object sender, EventArgs e)
        {
            adminChangeProfile adminChangeProfile = new adminChangeProfile(adminId);
            adminChangeProfile.Show();
        }
    } 
}