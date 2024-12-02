namespace try_messaging
{
    partial class tenant_paymentMODE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tenant_paymentMODE));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.totalBill = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.referenceText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.confirm_Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.roomNumberText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(118, 93);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(159, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // totalBill
            // 
            this.totalBill.AutoSize = true;
            this.totalBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalBill.Location = new System.Drawing.Point(152, 46);
            this.totalBill.Name = "totalBill";
            this.totalBill.Size = new System.Drawing.Size(144, 31);
            this.totalBill.TabIndex = 1;
            this.totalBill.Text = "00,000.00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(90, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Php";
            // 
            // referenceText
            // 
            this.referenceText.Location = new System.Drawing.Point(141, 328);
            this.referenceText.Name = "referenceText";
            this.referenceText.Size = new System.Drawing.Size(248, 20);
            this.referenceText.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 328);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Reference no.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(290, 26);
            this.label4.TabIndex = 5;
            this.label4.Text = "Please enter the reference number orupload the screen shot\r\nof your transaction";
            // 
            // confirm_Btn
            // 
            this.confirm_Btn.BackColor = System.Drawing.Color.White;
            this.confirm_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.confirm_Btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.confirm_Btn.Location = new System.Drawing.Point(118, 364);
            this.confirm_Btn.Name = "confirm_Btn";
            this.confirm_Btn.Size = new System.Drawing.Size(159, 25);
            this.confirm_Btn.TabIndex = 6;
            this.confirm_Btn.Text = "Confirm Payment";
            this.confirm_Btn.UseVisualStyleBackColor = false;
            this.confirm_Btn.Click += new System.EventHandler(this.confirm_Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Room number";
            // 
            // roomNumberText
            // 
            this.roomNumberText.Location = new System.Drawing.Point(139, 300);
            this.roomNumberText.Name = "roomNumberText";
            this.roomNumberText.Size = new System.Drawing.Size(250, 20);
            this.roomNumberText.TabIndex = 8;
            // 
            // tenant_paymentMODE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(414, 412);
            this.Controls.Add(this.roomNumberText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.confirm_Btn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.referenceText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.totalBill);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "tenant_paymentMODE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "tenant_paymentMODE";
            this.Load += new System.EventHandler(this.tenant_paymentMODE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label totalBill;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox referenceText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button confirm_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox roomNumberText;
    }
}