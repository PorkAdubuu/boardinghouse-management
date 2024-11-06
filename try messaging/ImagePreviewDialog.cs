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
    public partial class ImagePreviewDialog : Form
    {
        public ImagePreviewDialog()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        public ImagePreviewDialog(Image image)
        {
            InitializeComponent();
            pictureBoxPreview.Image = image;  // Set the image in the PictureBox
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the dialog when the button is clicked
        }
        private void ImagePreviewDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
