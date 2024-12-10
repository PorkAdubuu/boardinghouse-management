namespace try_messaging
{
    partial class resetPassword_code_admin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(resetPassword_code_admin));
            this.confirm_Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.codeText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // confirm_Btn
            // 
            this.confirm_Btn.BackColor = System.Drawing.Color.White;
            this.confirm_Btn.Location = new System.Drawing.Point(47, 81);
            this.confirm_Btn.Name = "confirm_Btn";
            this.confirm_Btn.Size = new System.Drawing.Size(75, 23);
            this.confirm_Btn.TabIndex = 8;
            this.confirm_Btn.Text = "Confirm";
            this.confirm_Btn.UseVisualStyleBackColor = false;
            this.confirm_Btn.Click += new System.EventHandler(this.confirm_Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Enter the code";
            // 
            // codeText
            // 
            this.codeText.Location = new System.Drawing.Point(12, 46);
            this.codeText.Name = "codeText";
            this.codeText.Size = new System.Drawing.Size(154, 20);
            this.codeText.TabIndex = 6;
            // 
            // resetPassword_code_admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(183, 126);
            this.Controls.Add(this.confirm_Btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.codeText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "resetPassword_code_admin";
            this.Text = "Admin";
            this.Load += new System.EventHandler(this.resetPassword_code_admin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button confirm_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox codeText;
    }
}