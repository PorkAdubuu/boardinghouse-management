namespace try_messaging
{
    partial class tenant_profile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tenant_profile));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PersonalInfoPanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.changePassPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Personal Information";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-2, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Welcome, Tenant!";
            // 
            // PersonalInfoPanel
            // 
            this.PersonalInfoPanel.AutoScroll = true;
            this.PersonalInfoPanel.BackColor = System.Drawing.Color.White;
            this.PersonalInfoPanel.Location = new System.Drawing.Point(-6, 89);
            this.PersonalInfoPanel.Name = "PersonalInfoPanel";
            this.PersonalInfoPanel.Size = new System.Drawing.Size(846, 194);
            this.PersonalInfoPanel.TabIndex = 2;
            this.PersonalInfoPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.PersonalInfoPanel_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 319);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Update Password";
            // 
            // changePassPanel
            // 
            this.changePassPanel.BackColor = System.Drawing.Color.White;
            this.changePassPanel.Location = new System.Drawing.Point(-6, 346);
            this.changePassPanel.Name = "changePassPanel";
            this.changePassPanel.Size = new System.Drawing.Size(846, 194);
            this.changePassPanel.TabIndex = 3;
            this.changePassPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.changePassPanel_Paint);
            // 
            // tenant_profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 539);
            this.Controls.Add(this.changePassPanel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PersonalInfoPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "tenant_profile";
            this.Text = "Profile";
            this.Load += new System.EventHandler(this.tenant_profile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PersonalInfoPanel;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Panel changePassPanel;
    }
}