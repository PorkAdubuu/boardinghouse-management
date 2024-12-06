namespace try_messaging
{
    partial class adminverificationCode
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
            this.SubmitCodeBtn = new System.Windows.Forms.Button();
            this.vcodeBtn = new System.Windows.Forms.Button();
            this.verificationText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SubmitCodeBtn
            // 
            this.SubmitCodeBtn.Location = new System.Drawing.Point(136, 65);
            this.SubmitCodeBtn.Name = "SubmitCodeBtn";
            this.SubmitCodeBtn.Size = new System.Drawing.Size(84, 23);
            this.SubmitCodeBtn.TabIndex = 109;
            this.SubmitCodeBtn.Text = "Submit code";
            this.SubmitCodeBtn.UseVisualStyleBackColor = true;
            this.SubmitCodeBtn.Click += new System.EventHandler(this.SubmitCodeBtn_Click);
            // 
            // vcodeBtn
            // 
            this.vcodeBtn.Location = new System.Drawing.Point(45, 65);
            this.vcodeBtn.Name = "vcodeBtn";
            this.vcodeBtn.Size = new System.Drawing.Size(84, 23);
            this.vcodeBtn.TabIndex = 108;
            this.vcodeBtn.Text = "Send code";
            this.vcodeBtn.UseVisualStyleBackColor = true;
            this.vcodeBtn.Click += new System.EventHandler(this.vcodeBtn_Click);
            // 
            // verificationText
            // 
            this.verificationText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verificationText.Location = new System.Drawing.Point(45, 33);
            this.verificationText.Name = "verificationText";
            this.verificationText.Size = new System.Drawing.Size(175, 26);
            this.verificationText.TabIndex = 107;
            // 
            // adminverificationCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(262, 127);
            this.Controls.Add(this.SubmitCodeBtn);
            this.Controls.Add(this.vcodeBtn);
            this.Controls.Add(this.verificationText);
            this.Name = "adminverificationCode";
            this.Text = "adminverificationCode";
            this.Load += new System.EventHandler(this.adminverificationCode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SubmitCodeBtn;
        private System.Windows.Forms.Button vcodeBtn;
        private System.Windows.Forms.TextBox verificationText;
    }
}