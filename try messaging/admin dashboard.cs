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
            admincomform.Show();
            this.Hide();
        }
    }
}
