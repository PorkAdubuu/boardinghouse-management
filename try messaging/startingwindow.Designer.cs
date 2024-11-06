namespace try_messaging
{
    partial class startingwindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(startingwindow));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.exitBtn = new System.Windows.Forms.PictureBox();
            this.userBtn = new System.Windows.Forms.PictureBox();
            this.adminBtn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adminBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::try_messaging.Properties.Resources.logo_real;
            this.pictureBox1.Location = new System.Drawing.Point(373, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(423, 169);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(240, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(410, 39);
            this.label1.TabIndex = 3;
            this.label1.Text = "Welcome to BoardMate!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(240, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(640, 39);
            this.label2.TabIndex = 4;
            this.label2.Text = "We’re glad you’re here. Let’s get started!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(243, 368);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(303, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Please select your role to continue:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitBtn.Image = global::try_messaging.Properties.Resources.EXIT_APP_BUTT;
            this.exitBtn.Location = new System.Drawing.Point(505, 564);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(185, 40);
            this.exitBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.exitBtn.TabIndex = 6;
            this.exitBtn.TabStop = false;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // userBtn
            // 
            this.userBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.userBtn.Image = global::try_messaging.Properties.Resources.USER_BUTT;
            this.userBtn.Location = new System.Drawing.Point(247, 414);
            this.userBtn.Name = "userBtn";
            this.userBtn.Size = new System.Drawing.Size(185, 40);
            this.userBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.userBtn.TabIndex = 7;
            this.userBtn.TabStop = false;
            this.userBtn.Click += new System.EventHandler(this.userBtn_Click);
            // 
            // adminBtn
            // 
            this.adminBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.adminBtn.Image = global::try_messaging.Properties.Resources.ADMIN_BUTT;
            this.adminBtn.Location = new System.Drawing.Point(247, 460);
            this.adminBtn.Name = "adminBtn";
            this.adminBtn.Size = new System.Drawing.Size(185, 40);
            this.adminBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.adminBtn.TabIndex = 8;
            this.adminBtn.TabStop = false;
            this.adminBtn.Click += new System.EventHandler(this.adminBtn_Click);
            // 
            // startingwindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.adminBtn);
            this.Controls.Add(this.userBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "startingwindow";
            this.Text = "BoardMate";
            this.Load += new System.EventHandler(this.startingwindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adminBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox exitBtn;
        private System.Windows.Forms.PictureBox userBtn;
        private System.Windows.Forms.PictureBox adminBtn;
    }
}

