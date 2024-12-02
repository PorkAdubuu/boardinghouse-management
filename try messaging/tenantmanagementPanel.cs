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
    public partial class tenantmanagementPanel : Form
    {
        private int adminID;
        public tenantmanagementPanel(int adminID)
        {
            InitializeComponent();
            this.adminID = adminID;
        }

        private void tenantmanagementPanel_Load(object sender, EventArgs e)
        {
            manageSelection.SelectedIndex = 0;
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

        private void SelectChoice()
        {
            if (manageSelection.SelectedItem.ToString() == "Add new tenant")
            {
                tenantmanagement tenantManagement = new tenantmanagement(adminID);
                LoadFormInPanel(tenantManagement);
            }
            else if (manageSelection.SelectedItem.ToString() == "Tenant lists")
            {
                tenant_lists tenant_Lists = new tenant_lists(adminID);
                LoadFormInPanel(tenant_Lists);
            }
        }

        private void manageSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectChoice();
        }
    }
}
