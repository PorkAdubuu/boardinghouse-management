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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.profile_picture = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.mail_icon = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.adminNameLabel = new System.Windows.Forms.TextBox();
            this.logoutBtn = new System.Windows.Forms.PictureBox();
            this.timeLabel = new System.Windows.Forms.Label();
            this.dashboard_Btn = new System.Windows.Forms.PictureBox();
            this.notification_Btn = new System.Windows.Forms.PictureBox();
            this.managetenant_Btn = new System.Windows.Forms.PictureBox();
            this.manageHouse_Btn = new System.Windows.Forms.PictureBox();
            this.payments_Btn = new System.Windows.Forms.PictureBox();
            this.maintenance_Btn = new System.Windows.Forms.PictureBox();
            this.analytics_Btn = new System.Windows.Forms.PictureBox();
            this.notificationGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.profile_picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mail_icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboard_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notification_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.managetenant_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.manageHouse_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.payments_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maintenance_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.analytics_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notificationGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(281, 53);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(885, 578);
            this.displayPanel.TabIndex = 2;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            // 
            // profile_picture
            // 
            this.profile_picture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.profile_picture.Image = ((System.Drawing.Image)(resources.GetObject("profile_picture.Image")));
            this.profile_picture.Location = new System.Drawing.Point(1136, 14);
            this.profile_picture.Name = "profile_picture";
            this.profile_picture.Size = new System.Drawing.Size(35, 33);
            this.profile_picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profile_picture.TabIndex = 8;
            this.profile_picture.TabStop = false;
            this.profile_picture.Click += new System.EventHandler(this.profile_picture_Click);
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
            this.mail_icon.Image = global::try_messaging.Properties.Resources.mail;
            this.mail_icon.Location = new System.Drawing.Point(960, 15);
            this.mail_icon.Name = "mail_icon";
            this.mail_icon.Size = new System.Drawing.Size(26, 26);
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
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.Color.Black;
            this.timeLabel.Location = new System.Drawing.Point(282, 27);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(230, 20);
            this.timeLabel.TabIndex = 63;
            this.timeLabel.Text = "MMMM dd, yyyy hh:mm:ss tt";
            // 
            // dashboard_Btn
            // 
            this.dashboard_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dashboard_Btn.Image = global::try_messaging.Properties.Resources.dashboard_plain_butt__2_;
            this.dashboard_Btn.Location = new System.Drawing.Point(35, 154);
            this.dashboard_Btn.Name = "dashboard_Btn";
            this.dashboard_Btn.Size = new System.Drawing.Size(151, 44);
            this.dashboard_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dashboard_Btn.TabIndex = 64;
            this.dashboard_Btn.TabStop = false;
            this.dashboard_Btn.Click += new System.EventHandler(this.dashboard_Btn_Click);
            // 
            // notification_Btn
            // 
            this.notification_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.notification_Btn.Image = global::try_messaging.Properties.Resources.bell;
            this.notification_Btn.Location = new System.Drawing.Point(915, 16);
            this.notification_Btn.Name = "notification_Btn";
            this.notification_Btn.Size = new System.Drawing.Size(29, 26);
            this.notification_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.notification_Btn.TabIndex = 66;
            this.notification_Btn.TabStop = false;
            this.notification_Btn.Click += new System.EventHandler(this.notification_Btn_Click);
            // 
            // managetenant_Btn
            // 
            this.managetenant_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.managetenant_Btn.Image = global::try_messaging.Properties.Resources.admin_manage_tenant_plain_butt;
            this.managetenant_Btn.Location = new System.Drawing.Point(35, 209);
            this.managetenant_Btn.Name = "managetenant_Btn";
            this.managetenant_Btn.Size = new System.Drawing.Size(151, 44);
            this.managetenant_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.managetenant_Btn.TabIndex = 67;
            this.managetenant_Btn.TabStop = false;
            this.managetenant_Btn.Click += new System.EventHandler(this.managetenant_Btn_Click);
            // 
            // manageHouse_Btn
            // 
            this.manageHouse_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.manageHouse_Btn.Image = global::try_messaging.Properties.Resources.admin_manage_house_plain_butt;
            this.manageHouse_Btn.Location = new System.Drawing.Point(35, 262);
            this.manageHouse_Btn.Name = "manageHouse_Btn";
            this.manageHouse_Btn.Size = new System.Drawing.Size(151, 44);
            this.manageHouse_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.manageHouse_Btn.TabIndex = 68;
            this.manageHouse_Btn.TabStop = false;
            this.manageHouse_Btn.Click += new System.EventHandler(this.manageHouse_Btn_Click);
            // 
            // payments_Btn
            // 
            this.payments_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.payments_Btn.Image = global::try_messaging.Properties.Resources.admin_tenant_payment_plain_butt;
            this.payments_Btn.Location = new System.Drawing.Point(35, 316);
            this.payments_Btn.Name = "payments_Btn";
            this.payments_Btn.Size = new System.Drawing.Size(151, 44);
            this.payments_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.payments_Btn.TabIndex = 69;
            this.payments_Btn.TabStop = false;
            this.payments_Btn.Click += new System.EventHandler(this.payments_Btn_Click);
            // 
            // maintenance_Btn
            // 
            this.maintenance_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.maintenance_Btn.Image = global::try_messaging.Properties.Resources.admin_maintenance_plain_butt;
            this.maintenance_Btn.Location = new System.Drawing.Point(35, 370);
            this.maintenance_Btn.Name = "maintenance_Btn";
            this.maintenance_Btn.Size = new System.Drawing.Size(151, 44);
            this.maintenance_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.maintenance_Btn.TabIndex = 70;
            this.maintenance_Btn.TabStop = false;
            this.maintenance_Btn.Click += new System.EventHandler(this.maintenance_Btn_Click);
            // 
            // analytics_Btn
            // 
            this.analytics_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.analytics_Btn.Image = global::try_messaging.Properties.Resources.admin_report_analytic_plain_butt;
            this.analytics_Btn.Location = new System.Drawing.Point(35, 424);
            this.analytics_Btn.Name = "analytics_Btn";
            this.analytics_Btn.Size = new System.Drawing.Size(151, 44);
            this.analytics_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.analytics_Btn.TabIndex = 71;
            this.analytics_Btn.TabStop = false;
            this.analytics_Btn.Click += new System.EventHandler(this.analytics_Btn_Click);
            // 
            // notificationGrid
            // 
            this.notificationGrid.AllowUserToAddRows = false;
            this.notificationGrid.AllowUserToDeleteRows = false;
            this.notificationGrid.AllowUserToResizeColumns = false;
            this.notificationGrid.AllowUserToResizeRows = false;
            this.notificationGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.notificationGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.notificationGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.notificationGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.notificationGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.notificationGrid.EnableHeadersVisualStyles = false;
            this.notificationGrid.GridColor = System.Drawing.Color.White;
            this.notificationGrid.Location = new System.Drawing.Point(800, 43);
            this.notificationGrid.MultiSelect = false;
            this.notificationGrid.Name = "notificationGrid";
            this.notificationGrid.ReadOnly = true;
            this.notificationGrid.RowHeadersVisible = false;
            this.notificationGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.notificationGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.notificationGrid.Size = new System.Drawing.Size(143, 210);
            this.notificationGrid.StandardTab = true;
            this.notificationGrid.TabIndex = 65;
            this.notificationGrid.Visible = false;
            // 
            // admin_dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.analytics_Btn);
            this.Controls.Add(this.maintenance_Btn);
            this.Controls.Add(this.payments_Btn);
            this.Controls.Add(this.manageHouse_Btn);
            this.Controls.Add(this.managetenant_Btn);
            this.Controls.Add(this.notificationGrid);
            this.Controls.Add(this.notification_Btn);
            this.Controls.Add(this.dashboard_Btn);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.profile_picture);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.adminNameLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.mail_icon);
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.pictureBox6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "admin_dashboard";
            this.Text = "Admin Dashboard";
            this.Load += new System.EventHandler(this.admin_dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.profile_picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mail_icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboard_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notification_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.managetenant_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.manageHouse_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.payments_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maintenance_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.analytics_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notificationGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.PictureBox profile_picture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox mail_icon;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox adminNameLabel;
        private System.Windows.Forms.PictureBox logoutBtn;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.PictureBox dashboard_Btn;
        private System.Windows.Forms.PictureBox notification_Btn;
        private System.Windows.Forms.PictureBox managetenant_Btn;
        private System.Windows.Forms.PictureBox manageHouse_Btn;
        private System.Windows.Forms.PictureBox payments_Btn;
        private System.Windows.Forms.PictureBox maintenance_Btn;
        private System.Windows.Forms.PictureBox analytics_Btn;
        private System.Windows.Forms.DataGridView notificationGrid;
    }
}