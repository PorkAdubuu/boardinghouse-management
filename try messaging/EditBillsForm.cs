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
    public partial class EditBillsForm : Form
    {
        private int tenantId; // Store the tenant ID for editing

        // Constructor that accepts tenantId as a parameter
        public EditBillsForm(int tenantId)
        {
            InitializeComponent();
            this.tenantId = tenantId; // Store the tenantId for later use
        }

        // Example of handling the Save button click
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Logic to save the bills for the selected tenant
            // You can use the tenantId to update the correct record in the database

            // For now, we'll just show a message indicating which tenant's bills are being updated
            MessageBox.Show($"Bills for Tenant ID {tenantId} have been updated.");

            // Close the Edit Bills form after saving
            this.Close();
        }

        // Example of handling the Cancel button click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the form without saving
        }

        // Form Load event to initialize form components if needed
        private void EditBillsForm_Load(object sender, EventArgs e)
        {
            // You can use the tenantId here to load specific tenant details if required
        }
        public EditBillsForm()
        {
            InitializeComponent();
            this.tenantId = tenantId;
        }

       
    }
}
