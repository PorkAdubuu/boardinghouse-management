using System;

namespace try_messaging
{
    partial class admin_dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(admin_dashboard));
            this.displayPanel = new System.Windows.Forms.Panel();
            this.profile_picture = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.mail_icon = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.adminNameLabel = new System.Windows.Forms.TextBox();
            this.logoutBtn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.profile_picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mail_icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(281, 53);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(855, 578);
            this.displayPanel.TabIndex = 2;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            // 
            // profile_picture
            // 
            this.profile_picture.Image = ((System.Drawing.Image)(resources.GetObject("profile_picture.Image")));
            this.profile_picture.Location = new System.Drawing.Point(1136, 14);
            this.profile_picture.Name = "profile_picture";
            this.profile_picture.Size = new System.Drawing.Size(35, 33);
            this.profile_picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profile_picture.TabIndex = 8;
            this.profile_picture.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(1094, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Admin";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(53, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "tenant management";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(53, 216);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "messenger";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(1, 2);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(283, 659);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 45;
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
            this.mail_icon.TabIndex = 46;
            this.mail_icon.TabStop = false;
            this.mail_icon.Click += new System.EventHandler(this.mail_icon_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::try_messaging.Properties.Resources.logo_real;
            this.pictureBox2.Location = new System.Drawing.Point(23, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(163, 79);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 47;
            this.pictureBox2.TabStop = false;
            // 
            // adminNameLabel
            // 
            this.adminNameLabel.BackColor = System.Drawing.Color.White;
            this.adminNameLabel.Location = new System.Drawing.Point(995, 18);
            this.adminNameLabel.Multiline = true;
            this.adminNameLabel.Name = "adminNameLabel";
            this.adminNameLabel.ReadOnly = true;
            this.adminNameLabel.Size = new System.Drawing.Size(135, 20);
            this.adminNameLabel.TabIndex = 48;
            // 
            // logoutBtn
            // 
            this.logoutBtn.BackColor = System.Drawing.Color.White;
            this.logoutBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logoutBtn.Image = global::try_messaging.Properties.Resources.LOG_OUT_BUTT;
            this.logoutBtn.Location = new System.Drawing.Point(53, 566);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(113, 32);
            this.logoutBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoutBtn.TabIndex = 49;
            this.logoutBtn.TabStop = false;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // admin_dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.profile_picture);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.adminNameLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.mail_icon);
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "admin_dashboard";
            this.Text = "Admin Dashboard";
            this.Load += new System.EventHandler(this.admin_dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.profile_picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mail_icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.PictureBox profile_picture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox mail_icon;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox adminNameLabel;
        private System.Windows.Forms.PictureBox logoutBtn;
    }
}