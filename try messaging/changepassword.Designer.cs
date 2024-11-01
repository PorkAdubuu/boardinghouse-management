namespace try_messaging
{
    partial class changepassword
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
            this.verificationText = new System.Windows.Forms.TextBox();
            this.changepasswordBtn = new System.Windows.Forms.Button();
            this.newpasswordText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // verificationText
            // 
            this.verificationText.Location = new System.Drawing.Point(325, 142);
            this.verificationText.Name = "verificationText";
            this.verificationText.Size = new System.Drawing.Size(100, 20);
            this.verificationText.TabIndex = 0;
            // 
            // changepasswordBtn
            // 
            this.changepasswordBtn.Location = new System.Drawing.Point(336, 222);
            this.changepasswordBtn.Name = "changepasswordBtn";
            this.changepasswordBtn.Size = new System.Drawing.Size(75, 23);
            this.changepasswordBtn.TabIndex = 1;
            this.changepasswordBtn.Text = "button1";
            this.changepasswordBtn.UseVisualStyleBackColor = true;
            this.changepasswordBtn.Click += new System.EventHandler(this.changepasswordBtn_Click);
            // 
            // newpasswordText
            // 
            this.newpasswordText.Location = new System.Drawing.Point(325, 178);
            this.newpasswordText.Name = "newpasswordText";
            this.newpasswordText.Size = new System.Drawing.Size(100, 20);
            this.newpasswordText.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "verification";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "new password";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newpasswordText);
            this.Controls.Add(this.changepasswordBtn);
            this.Controls.Add(this.verificationText);
            this.Name = "Form4";
            this.Text = "Form4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox verificationText;
        private System.Windows.Forms.Button changepasswordBtn;
        private System.Windows.Forms.TextBox newpasswordText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}