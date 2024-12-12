using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace try_messaging
{
    public partial class resetPassword_code_admin : Form
    {
        private string verificationCode;
        private string enteredEmail;

        public resetPassword_code_admin(string verificationCode, string enteredEmail)
        {
            InitializeComponent();
            this.verificationCode = verificationCode;
            this.enteredEmail = enteredEmail;
            this.CenterToParent();
        }

        private void resetPassword_code_admin_Load(object sender, EventArgs e)
        {
            // Optional: Any initialization code
        }

        private void confirm_Btn_Click(object sender, EventArgs e)
        {
            string enteredCode = codeText.Text.Trim();
            

            // Validate the entered code
            if (enteredCode != verificationCode)
            {
                MessageBox.Show("Invalid verification code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ChangePassword_admin changePassword_Admin = new ChangePassword_admin(enteredEmail);
                changePassword_Admin.Show();
                this.Close();
            }
           
            
        }

        
    }
}
