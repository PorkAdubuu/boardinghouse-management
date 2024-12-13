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
    public partial class bulletinForm : Form
    {
        public bulletinForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Open the provided PDF link in the default web browser
            string url = "https://www.tenancy.govt.nz/assets/Uploads/Tenancy/boarding-house-know-your-rights-english.pdf";
            System.Diagnostics.Process.Start(url);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {       
            string url = "https://newmillsproperties.com/smart-budgeting-for-tenants-a-comprehensive-guide/";
            System.Diagnostics.Process.Start(url);
        }
    }
}
