namespace try_messaging
{
    partial class verificaitoncode
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
            this.vcodeBtn = new System.Windows.Forms.Button();
            this.SubmitCodeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // verificationText
            // 
            this.verificationText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verificationText.Location = new System.Drawing.Point(26, 29);
            this.verificationText.Name = "verificationText";
            this.verificationText.Size = new System.Drawing.Size(100, 26);
            this.verificationText.TabIndex = 1;
            // 
            // vcodeBtn
            // 
            this.vcodeBtn.Location = new System.Drawing.Point(34, 69);
            this.vcodeBtn.Name = "vcodeBtn";
            this.vcodeBtn.Size = new System.Drawing.Size(84, 23);
            this.vcodeBtn.TabIndex = 6;
            this.vcodeBtn.Text = "Send code";
            this.vcodeBtn.UseVisualStyleBackColor = true;
            this.vcodeBtn.Click += new System.EventHandler(this.vcodeBtn_Click);
            // 
            // SubmitCodeBtn
            // 
            this.SubmitCodeBtn.Location = new System.Drawing.Point(34, 98);
            this.SubmitCodeBtn.Name = "SubmitCodeBtn";
            this.SubmitCodeBtn.Size = new System.Drawing.Size(84, 23);
            this.SubmitCodeBtn.TabIndex = 8;
            this.SubmitCodeBtn.Text = "Submit code";
            this.SubmitCodeBtn.UseVisualStyleBackColor = true;
            this.SubmitCodeBtn.Click += new System.EventHandler(this.SubmitCodeBtn_Click);
            // 
            // verificaitoncode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(830, 185);
            this.Controls.Add(this.SubmitCodeBtn);
            this.Controls.Add(this.vcodeBtn);
            this.Controls.Add(this.verificationText);
            this.Name = "verificaitoncode";
            this.Text = "verificaitoncode";
            this.Load += new System.EventHandler(this.verificaitoncode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox verificationText;
        private System.Windows.Forms.Button vcodeBtn;
        private System.Windows.Forms.Button SubmitCodeBtn;
    }
}