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
    public partial class adminsideform : Form
    {
        public adminsideform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tenantmanagement form5 = new tenantmanagement();
            form5.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            admincomform admincomform = new admincomform();
            admincomform.Show();
            this.Hide();


        
        }

        private void adminsideform_Load(object sender, EventArgs e)
        {

        }
    }
}
