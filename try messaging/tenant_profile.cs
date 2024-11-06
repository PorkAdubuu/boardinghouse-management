using System;
using System.Drawing;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class tenant_profile : Form
    {
        private DatabaseConnection dbConnection;
        private int tenantId;
        private string verificationCode; // To store the verification code

        public tenant_profile(string verificationCode, int tenantId) // Accept tenantId as a parameter
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#f7f7f7");
            dbConnection = new DatabaseConnection();
            this.tenantId = tenantId; // Set tenantId for this instance
            this.verificationCode = verificationCode;

            // Set the AutoScroll property of the panel to enable scrolling
            PersonalInfoPanel.AutoScroll = true;

            // Load the tenant personal information form inside the panel
            LoadTenantPersonalInformation();
            LoadTenantverificationcode();

            
        }
        

        private void LoadTenantPersonalInformation()
        {
            // Initialize the Personal_information_tenant form with tenantId and parentForm (tenant_profile)
            Personal_information_tenant personalInfoForm = new Personal_information_tenant(tenantId); // 'this' refers to tenant_profile form

            // Set up the form to be displayed inside the panel
            personalInfoForm.TopLevel = false;
            personalInfoForm.FormBorderStyle = FormBorderStyle.None;
            personalInfoForm.Dock = DockStyle.Fill;

            

            // Add the form to the panel and display it
            PersonalInfoPanel.Controls.Add(personalInfoForm);
            personalInfoForm.Show();
        }
        private void LoadTenantverificationcode()
        {
            // Initialize the VerificationCode form with the tenantId and a reference to tenant_profile
            verificaitoncode verificaitoncodeForm = new verificaitoncode(verificationCode, tenantId, this); // 'this' is tenant_profile form

            verificaitoncodeForm.TopLevel = false;
            verificaitoncodeForm.FormBorderStyle = FormBorderStyle.None;
            verificaitoncodeForm.Dock = DockStyle.Fill;

            // Clear any previous controls in the changePassPanel
            changePassPanel.Controls.Clear();

            // Add the form to the panel and display it
            changePassPanel.Controls.Add(verificaitoncodeForm);
            verificaitoncodeForm.Show();
        }








        private void tenant_profile_Load(object sender, EventArgs e)
        {
            // Load event for tenant_profile form, if needed
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            // Scroll event, no additional code needed as AutoScroll handles scrolling
        }

        private void PersonalInfoPanel_Paint(object sender, PaintEventArgs e)
        {
            // Paint event for the panel, if needed for custom drawing
        }

        private void changePassPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
