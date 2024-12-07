namespace try_messaging
{
    partial class declineWindow
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
            this.reasonCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.descriptionText = new System.Windows.Forms.RichTextBox();
            this.paidamountText = new System.Windows.Forms.Label();
            this.paymentAmountText = new System.Windows.Forms.TextBox();
            this.confirmDecline_Btn = new System.Windows.Forms.Button();
            this.tenantIDtext = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sendingLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.roomNumberText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // reasonCombo
            // 
            this.reasonCombo.FormattingEnabled = true;
            this.reasonCombo.Location = new System.Drawing.Point(84, 45);
            this.reasonCombo.Name = "reasonCombo";
            this.reasonCombo.Size = new System.Drawing.Size(121, 21);
            this.reasonCombo.TabIndex = 0;
            this.reasonCombo.SelectedIndexChanged += new System.EventHandler(this.reasonCombo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Reason:";
            // 
            // descriptionText
            // 
            this.descriptionText.Location = new System.Drawing.Point(16, 99);
            this.descriptionText.Name = "descriptionText";
            this.descriptionText.Size = new System.Drawing.Size(193, 96);
            this.descriptionText.TabIndex = 3;
            this.descriptionText.Text = "";
            // 
            // paidamountText
            // 
            this.paidamountText.AutoSize = true;
            this.paidamountText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paidamountText.Location = new System.Drawing.Point(13, 209);
            this.paidamountText.Name = "paidamountText";
            this.paidamountText.Size = new System.Drawing.Size(97, 16);
            this.paidamountText.TabIndex = 4;
            this.paidamountText.Text = "Paid amount:";
            this.paidamountText.Visible = false;
            // 
            // paymentAmountText
            // 
            this.paymentAmountText.Location = new System.Drawing.Point(111, 209);
            this.paymentAmountText.Name = "paymentAmountText";
            this.paymentAmountText.Size = new System.Drawing.Size(98, 20);
            this.paymentAmountText.TabIndex = 5;
            // 
            // confirmDecline_Btn
            // 
            this.confirmDecline_Btn.BackColor = System.Drawing.Color.White;
            this.confirmDecline_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.confirmDecline_Btn.ForeColor = System.Drawing.Color.Black;
            this.confirmDecline_Btn.Location = new System.Drawing.Point(50, 235);
            this.confirmDecline_Btn.Name = "confirmDecline_Btn";
            this.confirmDecline_Btn.Size = new System.Drawing.Size(119, 23);
            this.confirmDecline_Btn.TabIndex = 6;
            this.confirmDecline_Btn.Text = "Confirm";
            this.confirmDecline_Btn.UseVisualStyleBackColor = false;
            this.confirmDecline_Btn.Click += new System.EventHandler(this.confirmDecline_Btn_Click);
            // 
            // tenantIDtext
            // 
            this.tenantIDtext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(49)))), ((int)(((byte)(115)))));
            this.tenantIDtext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tenantIDtext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(49)))), ((int)(((byte)(115)))));
            this.tenantIDtext.Location = new System.Drawing.Point(3, 1);
            this.tenantIDtext.Name = "tenantIDtext";
            this.tenantIDtext.ReadOnly = true;
            this.tenantIDtext.Size = new System.Drawing.Size(45, 13);
            this.tenantIDtext.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Description:";
            // 
            // sendingLabel
            // 
            this.sendingLabel.AutoSize = true;
            this.sendingLabel.ForeColor = System.Drawing.Color.White;
            this.sendingLabel.Location = new System.Drawing.Point(75, 270);
            this.sendingLabel.Name = "sendingLabel";
            this.sendingLabel.Size = new System.Drawing.Size(73, 13);
            this.sendingLabel.TabIndex = 65;
            this.sendingLabel.Text = "Please wait....";
            this.sendingLabel.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(17, 286);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(178, 10);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 64;
            this.progressBar.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 66;
            this.label1.Text = "Room number:";
            // 
            // roomNumberText
            // 
            this.roomNumberText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(49)))), ((int)(((byte)(115)))));
            this.roomNumberText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.roomNumberText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roomNumberText.ForeColor = System.Drawing.Color.White;
            this.roomNumberText.Location = new System.Drawing.Point(126, 17);
            this.roomNumberText.Name = "roomNumberText";
            this.roomNumberText.ReadOnly = true;
            this.roomNumberText.Size = new System.Drawing.Size(61, 17);
            this.roomNumberText.TabIndex = 67;
            // 
            // declineWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(49)))), ((int)(((byte)(115)))));
            this.ClientSize = new System.Drawing.Size(221, 311);
            this.Controls.Add(this.roomNumberText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendingLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tenantIDtext);
            this.Controls.Add(this.confirmDecline_Btn);
            this.Controls.Add(this.paymentAmountText);
            this.Controls.Add(this.paidamountText);
            this.Controls.Add(this.descriptionText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.reasonCombo);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "declineWindow";
            this.Text = "declineWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.declineWindow_FormClosed);
            this.Load += new System.EventHandler(this.declineWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox reasonCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox descriptionText;
        private System.Windows.Forms.Label paidamountText;
        private System.Windows.Forms.TextBox paymentAmountText;
        private System.Windows.Forms.Button confirmDecline_Btn;
        private System.Windows.Forms.TextBox tenantIDtext;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label sendingLabel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox roomNumberText;
    }
}