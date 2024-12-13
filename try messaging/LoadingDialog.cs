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
    public partial class LoadingDialog : Form
    {
        public LoadingDialog()
        {
            InitializeComponent();
            this.ControlBox = false; // Disable close button
            this.CenterToParent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Text = "Processing...";
        }

        private void LoadingDialog_Load(object sender, EventArgs e)
        {

        }
        public void UpdateStatus(string statusMessage)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => { statusLabel.Text = statusMessage; }));
            }
            else
            {
                statusLabel.Text = statusMessage;
            }
        }
    }
}
