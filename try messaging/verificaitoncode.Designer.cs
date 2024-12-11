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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(verificaitoncode));
            this.verificationText = new System.Windows.Forms.TextBox();
            this.SubmitCodeBtn = new System.Windows.Forms.Button();
            this.sendcode_Btn = new System.Windows.Forms.PictureBox();
            this.sendingLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.sendcode_Btn)).BeginInit();
            this.SuspendLayout();
            // 
            // verificationText
            // 
            this.verificationText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verificationText.Location = new System.Drawing.Point(339, 69);
            this.verificationText.Name = "verificationText";
            this.verificationText.Size = new System.Drawing.Size(124, 26);
            this.verificationText.TabIndex = 1;
            // 
            // SubmitCodeBtn
            // 
            this.SubmitCodeBtn.BackColor = System.Drawing.Color.White;
            this.SubmitCodeBtn.ForeColor = System.Drawing.Color.Black;
            this.SubmitCodeBtn.Location = new System.Drawing.Point(360, 101);
            this.SubmitCodeBtn.Name = "SubmitCodeBtn";
            this.SubmitCodeBtn.Size = new System.Drawing.Size(84, 23);
            this.SubmitCodeBtn.TabIndex = 8;
            this.SubmitCodeBtn.Text = "Submit code";
            this.SubmitCodeBtn.UseVisualStyleBackColor = false;
            this.SubmitCodeBtn.Click += new System.EventHandler(this.SubmitCodeBtn_Click);
            // 
            // sendcode_Btn
            // 
            this.sendcode_Btn.Image = global::try_messaging.Properties.Resources.padlock__2_;
            this.sendcode_Btn.Location = new System.Drawing.Point(319, 37);
            this.sendcode_Btn.Name = "sendcode_Btn";
            this.sendcode_Btn.Size = new System.Drawing.Size(167, 107);
            this.sendcode_Btn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sendcode_Btn.TabIndex = 9;
            this.sendcode_Btn.TabStop = false;
            this.sendcode_Btn.Click += new System.EventHandler(this.sendcode_Btn_Click);
            // 
            // sendingLabel
            // 
            this.sendingLabel.AutoSize = true;
            this.sendingLabel.ForeColor = System.Drawing.Color.White;
            this.sendingLabel.Location = new System.Drawing.Point(370, 147);
            this.sendingLabel.Name = "sendingLabel";
            this.sendingLabel.Size = new System.Drawing.Size(82, 13);
            this.sendingLabel.TabIndex = 65;
            this.sendingLabel.Text = "Sending code...";
            this.sendingLabel.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(305, 163);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 10);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 64;
            this.progressBar.Visible = false;
            // 
            // verificaitoncode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(841, 185);
            this.Controls.Add(this.sendcode_Btn);
            this.Controls.Add(this.sendingLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.SubmitCodeBtn);
            this.Controls.Add(this.verificationText);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "verificaitoncode";
            this.Load += new System.EventHandler(this.verificaitoncode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sendcode_Btn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox verificationText;
        private System.Windows.Forms.Button SubmitCodeBtn;
        private System.Windows.Forms.PictureBox sendcode_Btn;
        private System.Windows.Forms.Label sendingLabel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}