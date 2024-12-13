namespace try_messaging
{
    partial class ChangePassword_admin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePassword_admin));
            this.label3 = new System.Windows.Forms.Label();
            this.submit_Btn = new System.Windows.Forms.Button();
            this.showPassword = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.confirmPassText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newpasswordText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(71, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 20);
            this.label3.TabIndex = 42;
            this.label3.Text = "Update your password";
            // 
            // submit_Btn
            // 
            this.submit_Btn.BackColor = System.Drawing.Color.White;
            this.submit_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submit_Btn.Location = new System.Drawing.Point(140, 154);
            this.submit_Btn.Name = "submit_Btn";
            this.submit_Btn.Size = new System.Drawing.Size(75, 23);
            this.submit_Btn.TabIndex = 41;
            this.submit_Btn.Text = "Submit";
            this.submit_Btn.UseVisualStyleBackColor = false;
            this.submit_Btn.Click += new System.EventHandler(this.submit_Btn_Click);
            // 
            // showPassword
            // 
            this.showPassword.BackColor = System.Drawing.Color.White;
            this.showPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showPassword.Image = ((System.Drawing.Image)(resources.GetObject("showPassword.Image")));
            this.showPassword.Location = new System.Drawing.Point(301, 70);
            this.showPassword.Name = "showPassword";
            this.showPassword.Size = new System.Drawing.Size(19, 18);
            this.showPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.showPassword.TabIndex = 40;
            this.showPassword.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 39;
            this.label1.Text = "Confirm password:";
            // 
            // confirmPassText
            // 
            this.confirmPassText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmPassText.Location = new System.Drawing.Point(128, 108);
            this.confirmPassText.Name = "confirmPassText";
            this.confirmPassText.PasswordChar = '*';
            this.confirmPassText.Size = new System.Drawing.Size(193, 22);
            this.confirmPassText.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 16);
            this.label2.TabIndex = 37;
            this.label2.Text = "New password:";
            // 
            // newpasswordText
            // 
            this.newpasswordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newpasswordText.Location = new System.Drawing.Point(128, 68);
            this.newpasswordText.Name = "newpasswordText";
            this.newpasswordText.PasswordChar = '*';
            this.newpasswordText.Size = new System.Drawing.Size(193, 22);
            this.newpasswordText.TabIndex = 36;
            // 
            // ChangePassword_admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(328, 199);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.submit_Btn);
            this.Controls.Add(this.showPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.confirmPassText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newpasswordText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangePassword_admin";
            this.Text = "Admin";
            this.Load += new System.EventHandler(this.ChangePassword_admin_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button submit_Btn;
        private System.Windows.Forms.PictureBox showPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox confirmPassText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newpasswordText;
    }
}