namespace try_messaging
{
    partial class newPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(newPassword));
            this.label1 = new System.Windows.Forms.Label();
            this.confirmPassText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newpasswordText = new System.Windows.Forms.TextBox();
            this.showPassword = new System.Windows.Forms.PictureBox();
            this.submit_Btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Confirm password";
            // 
            // confirmPassText
            // 
            this.confirmPassText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirmPassText.Location = new System.Drawing.Point(125, 95);
            this.confirmPassText.Name = "confirmPassText";
            this.confirmPassText.PasswordChar = '*';
            this.confirmPassText.Size = new System.Drawing.Size(193, 22);
            this.confirmPassText.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "new password";
            // 
            // newpasswordText
            // 
            this.newpasswordText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newpasswordText.Location = new System.Drawing.Point(125, 55);
            this.newpasswordText.Name = "newpasswordText";
            this.newpasswordText.PasswordChar = '*';
            this.newpasswordText.Size = new System.Drawing.Size(193, 22);
            this.newpasswordText.TabIndex = 7;
            // 
            // showPassword
            // 
            this.showPassword.BackColor = System.Drawing.Color.White;
            this.showPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showPassword.Image = ((System.Drawing.Image)(resources.GetObject("showPassword.Image")));
            this.showPassword.Location = new System.Drawing.Point(298, 57);
            this.showPassword.Name = "showPassword";
            this.showPassword.Size = new System.Drawing.Size(19, 18);
            this.showPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.showPassword.TabIndex = 33;
            this.showPassword.TabStop = false;
            this.showPassword.Click += new System.EventHandler(this.showPassword_Click);
            // 
            // submit_Btn
            // 
            this.submit_Btn.BackColor = System.Drawing.Color.White;
            this.submit_Btn.Location = new System.Drawing.Point(137, 141);
            this.submit_Btn.Name = "submit_Btn";
            this.submit_Btn.Size = new System.Drawing.Size(75, 23);
            this.submit_Btn.TabIndex = 34;
            this.submit_Btn.Text = "Submit";
            this.submit_Btn.UseVisualStyleBackColor = false;
            this.submit_Btn.Click += new System.EventHandler(this.submit_Btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(68, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 20);
            this.label3.TabIndex = 35;
            this.label3.Text = "Update your password";
            // 
            // newPassword
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
            this.Name = "newPassword";
            this.Load += new System.EventHandler(this.newPassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox confirmPassText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newpasswordText;
        private System.Windows.Forms.PictureBox showPassword;
        private System.Windows.Forms.Button submit_Btn;
        private System.Windows.Forms.Label label3;
    }
}