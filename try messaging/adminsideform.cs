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
            Form5 form5 = new Form5();
            form5.Show();
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
