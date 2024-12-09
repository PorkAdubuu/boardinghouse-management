using System;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class resetPassword_code : Form
    {
        private string expectedCode; // Store the expected verification code
        private string tenantEmail; // Store the tenant's email

        public resetPassword_code(string verificationCode, string email)
        {
            InitializeComponent();
            expectedCode = verificationCode; // Pass the code generated earlier
            tenantEmail = email; // Pass the tenant's email
            this.CenterToParent();
        }

        private void confirm_Btn_Click(object sender, EventArgs e)
        {
            string enteredCode = codeText.Text.Trim(); // Get the code entered by the user

            if (string.IsNullOrEmpty(enteredCode))
            {
                MessageBox.Show("Please enter the verification code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (enteredCode == expectedCode)
            {
                MessageBox.Show("Verification successful! You may now reset your password.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open the reset password form
                newPassword resetPassword = new newPassword(tenantEmail); // Pass the tenant's email
                resetPassword.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid verification code. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetPassword_code_Load(object sender, EventArgs e)
        {

        }
    }
}
