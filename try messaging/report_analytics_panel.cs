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
    public partial class report_analytics_panel : Form
    {
       
        
        public report_analytics_panel()
        {
            InitializeComponent();
            
        }

        private void report_analytics_panel_Load(object sender, EventArgs e)
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
            if (manageSelection.SelectedItem.ToString() == "Income Report")
            {
                admin_income_report admin_Income_Report = new admin_income_report();
                LoadFormInPanel(admin_Income_Report);
            }
            else if (manageSelection.SelectedItem.ToString() == "Tenant Report")
            {
                tenant tenant_Reports = new tenant();
                LoadFormInPanel(tenant_Reports);
            }
            else if (manageSelection.SelectedItem.ToString() == "Bills Report")
            {
               bill_report bill_Report = new bill_report();
               LoadFormInPanel(bill_Report);
            }
        }

        

        private void manageSelection_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SelectChoice();
        }
    }
}
