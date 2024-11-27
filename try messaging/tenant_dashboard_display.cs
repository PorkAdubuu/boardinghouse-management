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

    }
}
