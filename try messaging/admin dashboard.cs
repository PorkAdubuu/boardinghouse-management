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
    public partial class admin_dashboard : Form
    {
        public admin_dashboard()
        {
            InitializeComponent();
            this.CenterToScreen();
            //color
            this.BackColor = ColorTranslator.FromHtml("#ffffff");
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
        private void admin_dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tenantmanagement tenantmanagement = new tenantmanagement();
            tenantmanagement.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            admincomform admincomform = new admincomform();
            
            LoadFormInPanel(admincomform);
        }

        private void displayPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
