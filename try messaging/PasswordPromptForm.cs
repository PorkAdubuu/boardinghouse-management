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
    public partial class PasswordPromptForm : Form
    {
        public string AdminPassword { get; private set; }
        public PasswordPromptForm()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        private void PasswordPromptForm_Load(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            // Capture the password entered in the textbox
            AdminPassword = passwordTextBox.Text;

            // Close the form once the password is captured
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
