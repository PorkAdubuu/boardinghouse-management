namespace try_messaging
{
    partial class tenant_dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tenant_dashboard));
            this.displayPanel = new System.Windows.Forms.Panel();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.mail_icon = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.profilePic = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tenantNameLabel = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mail_icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(281, 61);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(885, 578);
            this.displayPanel.TabIndex = 4;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(1, 2);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(283, 659);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 46;
            this.pictureBox6.TabStop = false;
            // 
            // mail_icon
            // 
            this.mail_icon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mail_icon.Image = ((System.Drawing.Image)(resources.GetObject("mail_icon.Image")));
            this.mail_icon.Location = new System.Drawing.Point(964, 19);
            this.mail_icon.Name = "mail_icon";
            this.mail_icon.Size = new System.Drawing.Size(25, 19);
            this.mail_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mail_icon.TabIndex = 50;
            this.mail_icon.TabStop = false;
            this.mail_icon.Click += new System.EventHandler(this.mail_icon_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(1090, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 49;
            this.label2.Text = "Tenant";
            // 
            // profilePic
            // 
            this.profilePic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.profilePic.Image = ((System.Drawing.Image)(resources.GetObject("profilePic.Image")));
            this.profilePic.Location = new System.Drawing.Point(1136, 14);
            this.profilePic.Name = "profilePic";
            this.profilePic.Size = new System.Drawing.Size(35, 33);
            this.profilePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilePic.TabIndex = 47;
            this.profilePic.TabStop = false;
            this.profilePic.Click += new System.EventHandler(this.profilePic_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::try_messaging.Properties.Resources.logo_real;
            this.pictureBox2.Location = new System.Drawing.Point(23, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(163, 79);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 51;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::try_messaging.Properties.Resources.LOG_OUT_BUTT;
            this.pictureBox1.Location = new System.Drawing.Point(53, 566);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(113, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 56;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // tenantNameLabel
            // 
            this.tenantNameLabel.BackColor = System.Drawing.Color.White;
            this.tenantNameLabel.Location = new System.Drawing.Point(995, 18);
            this.tenantNameLabel.Name = "tenantNameLabel";
            this.tenantNameLabel.Size = new System.Drawing.Size(135, 20);
            this.tenantNameLabel.TabIndex = 54;
            // 
            // tenant_dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.profilePic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tenantNameLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.mail_icon);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.displayPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "tenant_dashboard";
            this.Text = "Tenant Dashboard";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mail_icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox mail_icon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox profilePic;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tenantNameLabel;
    }
}