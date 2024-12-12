namespace try_messaging
{
    partial class forgotPass_window
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(forgotPass_window));
            this.emailText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendcode_Btn = new System.Windows.Forms.Button();
            this.sendingLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // emailText
            // 
            this.emailText.Location = new System.Drawing.Point(41, 43);
            this.emailText.Name = "emailText";
            this.emailText.Size = new System.Drawing.Size(154, 20);
            this.emailText.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter your email address";
            // 
            // sendcode_Btn
            // 
            this.sendcode_Btn.BackColor = System.Drawing.Color.White;
            this.sendcode_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sendcode_Btn.Location = new System.Drawing.Point(80, 79);
            this.sendcode_Btn.Name = "sendcode_Btn";
            this.sendcode_Btn.Size = new System.Drawing.Size(75, 23);
            this.sendcode_Btn.TabIndex = 2;
            this.sendcode_Btn.Text = "Send code";
            this.sendcode_Btn.UseVisualStyleBackColor = false;
            this.sendcode_Btn.Click += new System.EventHandler(this.sendcode_Btn_Click);
            // 
            // sendingLabel
            // 
            this.sendingLabel.AutoSize = true;
            this.sendingLabel.Location = new System.Drawing.Point(88, 115);
            this.sendingLabel.Name = "sendingLabel";
            this.sendingLabel.Size = new System.Drawing.Size(67, 13);
            this.sendingLabel.TabIndex = 65;
            this.sendingLabel.Text = "Sending.......";
            this.sendingLabel.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(41, 131);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(141, 10);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 64;
            this.progressBar.Visible = false;
            // 
            // forgotPass_window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(233, 163);
            this.Controls.Add(this.sendingLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.sendcode_Btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.emailText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "forgotPass_window";
            this.Load += new System.EventHandler(this.forgotPass_window_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox emailText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendcode_Btn;
        private System.Windows.Forms.Label sendingLabel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}