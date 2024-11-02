using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class tenant_dashboard : Form
    {
        private string verificationCode; // Define verificationCode at the class level
        private int tenantId; // Store the tenant's ID

        public tenant_dashboard(int tenantId)
        {
            InitializeComponent();
            this.tenantId = tenantId; // Store the tenant's ID
        }

        private void LoadFormInPanel(Form childForm)
        {
            // Clear existing controls in the panel
            displayPanel.Controls.Clear();

            // Set up the child form's properties
            childForm.TopLevel = false; // Make it a non-top-level form
            childForm.FormBorderStyle = FormBorderStyle.None; // Remove the form border
            childForm.Dock = DockStyle.Fill; // Make it fill the panel

            // Add the form to the panel
            displayPanel.Controls.Add(childForm);
            displayPanel.Tag = childForm;

            // Display the form
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            changepassword form4 = new changepassword(verificationCode, tenantId);
            LoadFormInPanel(form4); // Load changepassword form in displayPanel
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            tenantcomform tenantcomform = new tenantcomform(tenantId);
            LoadFormInPanel(tenantcomform); // Load tenantcomform form in displayPanel
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
