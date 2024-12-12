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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.mail_icon = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.profilePic = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tenantNameLabel = new System.Windows.Forms.TextBox();
            this.dashboard_Btn = new System.Windows.Forms.PictureBox();
            this.profile_Btn = new System.Windows.Forms.PictureBox();
            this.payment_Btn = new System.Windows.Forms.PictureBox();
            this.maintenance_Btn = new System.Windows.Forms.PictureBox();
            this.timeLabel = new System.Windows.Forms.Label();
            this.notification_Btn = new System.Windows.Forms.PictureBox();
            this.notificationsTable = new System.Windows.Forms.DataGridView();
            this.notifPanel = new System.Windows.Forms.Panel();
            this.clear_Btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mail_icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboard_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profile_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.payment_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maintenance_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notification_Btn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notificationsTable)).BeginInit();
            this.notifPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(281, 61);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(885, 578);
            this.displayPanel.TabIndex = 4;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            this.displayPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.displayPanel_MouseClick);
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
            this.mail_icon.Image = global::try_messaging.Properties.Resources.mail;
            this.mail_icon.Location = new System.Drawing.Point(960, 15);
            this.mail_icon.Name = "mail_icon";
            this.mail_icon.Size = new System.Drawing.Size(29, 26);
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
            // dashboard_Btn
            // 
            this.dashboard_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dashboard_Btn.Image = global::try_messaging.Properties.Resources.dashboard_plain_butt__2_;
            this.dashboard_Btn.Location = new System.Drawing.Point(35, 154);
            this.dashboard_Btn.Name = "dashboard_Btn";
            this.dashboard_Btn.Size = new System.Drawing.Size(151, 44);
            this.dashboard_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dashboard_Btn.TabIndex = 58;
            this.dashboard_Btn.TabStop = false;
            this.dashboard_Btn.Click += new System.EventHandler(this.dashboard_Btn_Click_1);
            // 
            // profile_Btn
            // 
            this.profile_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.profile_Btn.Image = global::try_messaging.Properties.Resources.profile_plain_butt__1_;
            this.profile_Btn.Location = new System.Drawing.Point(35, 209);
            this.profile_Btn.Name = "profile_Btn";
            this.profile_Btn.Size = new System.Drawing.Size(151, 44);
            this.profile_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profile_Btn.TabIndex = 59;
            this.profile_Btn.TabStop = false;
            this.profile_Btn.Click += new System.EventHandler(this.profile_Btn_Click);
            // 
            // payment_Btn
            // 
            this.payment_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.payment_Btn.Image = global::try_messaging.Properties.Resources.payment_plain_butt;
            this.payment_Btn.Location = new System.Drawing.Point(35, 264);
            this.payment_Btn.Name = "payment_Btn";
            this.payment_Btn.Size = new System.Drawing.Size(151, 44);
            this.payment_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.payment_Btn.TabIndex = 60;
            this.payment_Btn.TabStop = false;
            this.payment_Btn.Click += new System.EventHandler(this.payment_Btn_Click);
            // 
            // maintenance_Btn
            // 
            this.maintenance_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.maintenance_Btn.Image = global::try_messaging.Properties.Resources.maintenance_plain_butt_11;
            this.maintenance_Btn.Location = new System.Drawing.Point(35, 319);
            this.maintenance_Btn.Name = "maintenance_Btn";
            this.maintenance_Btn.Size = new System.Drawing.Size(151, 44);
            this.maintenance_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.maintenance_Btn.TabIndex = 61;
            this.maintenance_Btn.TabStop = false;
            this.maintenance_Btn.Click += new System.EventHandler(this.maintenance_Btn_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.Color.Black;
            this.timeLabel.Location = new System.Drawing.Point(282, 27);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(230, 20);
            this.timeLabel.TabIndex = 62;
            this.timeLabel.Text = "MMMM dd, yyyy hh:mm:ss tt";
            // 
            // notification_Btn
            // 
            this.notification_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.notification_Btn.Image = global::try_messaging.Properties.Resources.bell;
            this.notification_Btn.Location = new System.Drawing.Point(915, 16);
            this.notification_Btn.Name = "notification_Btn";
            this.notification_Btn.Size = new System.Drawing.Size(29, 26);
            this.notification_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.notification_Btn.TabIndex = 63;
            this.notification_Btn.TabStop = false;
            this.notification_Btn.Click += new System.EventHandler(this.notification_Btn_Click);
            // 
            // notificationsTable
            // 
            this.notificationsTable.BackgroundColor = System.Drawing.Color.White;
            this.notificationsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.notificationsTable.Location = new System.Drawing.Point(-1, -1);
            this.notificationsTable.MultiSelect = false;
            this.notificationsTable.Name = "notificationsTable";
            this.notificationsTable.ReadOnly = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            this.notificationsTable.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.notificationsTable.Size = new System.Drawing.Size(200, 237);
            this.notificationsTable.TabIndex = 65;
            // 
            // notifPanel
            // 
            this.notifPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.notifPanel.Controls.Add(this.clear_Btn);
            this.notifPanel.Controls.Add(this.notificationsTable);
            this.notifPanel.Location = new System.Drawing.Point(744, 43);
            this.notifPanel.Name = "notifPanel";
            this.notifPanel.Size = new System.Drawing.Size(200, 265);
            this.notifPanel.TabIndex = 64;
            // 
            // clear_Btn
            // 
            this.clear_Btn.BackColor = System.Drawing.Color.White;
            this.clear_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clear_Btn.Location = new System.Drawing.Point(61, 239);
            this.clear_Btn.Name = "clear_Btn";
            this.clear_Btn.Size = new System.Drawing.Size(75, 23);
            this.clear_Btn.TabIndex = 66;
            this.clear_Btn.Text = "Clear";
            this.clear_Btn.UseVisualStyleBackColor = false;
            this.clear_Btn.Click += new System.EventHandler(this.clear_Btn_Click);
            // 
            // tenant_dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.notifPanel);
            this.Controls.Add(this.mail_icon);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.notification_Btn);
            this.Controls.Add(this.maintenance_Btn);
            this.Controls.Add(this.payment_Btn);
            this.Controls.Add(this.profile_Btn);
            this.Controls.Add(this.dashboard_Btn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.profilePic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tenantNameLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.displayPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "tenant_dashboard";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tenant_dashboard_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mail_icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dashboard_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profile_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.payment_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maintenance_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notification_Btn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notificationsTable)).EndInit();
            this.notifPanel.ResumeLayout(false);
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
        private System.Windows.Forms.PictureBox dashboard_Btn;
        private System.Windows.Forms.PictureBox profile_Btn;
        private System.Windows.Forms.PictureBox payment_Btn;
        private System.Windows.Forms.PictureBox maintenance_Btn;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.PictureBox notification_Btn;
        private System.Windows.Forms.DataGridView notificationsTable;
        private System.Windows.Forms.Panel notifPanel;
        private System.Windows.Forms.Button clear_Btn;
    }
}