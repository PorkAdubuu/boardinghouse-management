namespace try_messaging
{
    partial class adminChangePasswordd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminChangePasswordd));
            this.showPassword = new System.Windows.Forms.PictureBox();
            this.submitPassBtn = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.currentPassText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.confirmPassText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newpasswordText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.submitPassBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // showPassword
            // 
            this.showPassword.BackColor = System.Drawing.Color.White;
            this.showPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showPassword.Image = ((System.Drawing.Image)(resources.GetObject("showPassword.Image")));
            this.showPassword.Location = new System.Drawing.Point(238, 15);
            this.showPassword.Name = "showPassword";
            this.showPassword.Size = new System.Drawing.Size(19, 18);
            this.showPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.showPassword.TabIndex = 40;
            this.showPassword.TabStop = false;
            this.showPassword.Click += new System.EventHandler(this.showPassword_Click);
            // 
            // submitPassBtn
            // 
            this.submitPassBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitPassBtn.Image = global::try_messaging.Properties.Resources.SUBMIT_BUTT;
            this.submitPassBtn.Location = new System.Drawing.Point(78, 100);
            this.submitPassBtn.Name = "submitPassBtn";
            this.submitPassBtn.Size = new System.Drawing.Size(103, 25);
            this.submitPassBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.submitPassBtn.TabIndex = 39;
            this.submitPassBtn.TabStop = false;
            this.submitPassBtn.Click += new System.EventHandler(this.submitPassBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 16);
            this.label3.TabIndex = 38;
            this.label3.Text = "Current password";
            // 
            // currentPassText
            // 
            this.currentPassText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentPassText.Location = new System.Drawing.Point(120, 13);
            this.currentPassText.Name = "currentPassText";
            this.currentPassText.PasswordChar = '*';
            this.currentPassText.Size = new System.Drawing.Size(139, 22);
            this.currentPassText.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 36;
            this.label1.Text = "Confirm password";
            // 
            // confirmPassText
            // 
            this.confirmPassText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmPassText.Location = new System.Drawing.Point(120, 70);
            this.confirmPassText.Name = "confirmPassText";
            this.confirmPassText.PasswordChar = '*';
            this.confirmPassText.Size = new System.Drawing.Size(139, 22);
            this.confirmPassText.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 34;
            this.label2.Text = "New password";
            // 
            // newpasswordText
            // 
            this.newpasswordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newpasswordText.Location = new System.Drawing.Point(120, 42);
            this.newpasswordText.Name = "newpasswordText";
            this.newpasswordText.PasswordChar = '*';
            this.newpasswordText.Size = new System.Drawing.Size(139, 22);
            this.newpasswordText.TabIndex = 33;
            // 
            // adminChangePasswordd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(262, 127);
            this.Controls.Add(this.showPassword);
            this.Controls.Add(this.submitPassBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.currentPassText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.confirmPassText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newpasswordText);
            this.Name = "adminChangePasswordd";
            this.Text = "adminChangePasswordd";
            this.Load += new System.EventHandler(this.adminChangePasswordd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.submitPassBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox showPassword;
        private System.Windows.Forms.PictureBox submitPassBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox currentPassText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox confirmPassText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newpasswordText;
    }
}