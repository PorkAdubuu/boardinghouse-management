namespace try_messaging
{
    partial class tenantcomform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tenantcomform));
            this.conversationBox = new System.Windows.Forms.RichTextBox();
            this.typeMessage = new System.Windows.Forms.RichTextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.adminprofile = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.adminprofile)).BeginInit();
            this.SuspendLayout();
            // 
            // conversationBox
            // 
            this.conversationBox.BackColor = System.Drawing.Color.White;
            this.conversationBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conversationBox.Location = new System.Drawing.Point(3, 55);
            this.conversationBox.Name = "conversationBox";
            this.conversationBox.ReadOnly = true;
            this.conversationBox.Size = new System.Drawing.Size(623, 282);
            this.conversationBox.TabIndex = 12;
            this.conversationBox.Text = "";
            this.conversationBox.TextChanged += new System.EventHandler(this.conversationBox_TextChanged);
            // 
            // typeMessage
            // 
            this.typeMessage.BackColor = System.Drawing.Color.White;
            this.typeMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeMessage.Location = new System.Drawing.Point(3, 343);
            this.typeMessage.Name = "typeMessage";
            this.typeMessage.Size = new System.Drawing.Size(562, 78);
            this.typeMessage.TabIndex = 11;
            this.typeMessage.Text = "";
            this.typeMessage.TextChanged += new System.EventHandler(this.typeMessage_TextChanged);
            // 
            // sendBtn
            // 
            this.sendBtn.BackColor = System.Drawing.Color.White;
            this.sendBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendBtn.Location = new System.Drawing.Point(569, 343);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(57, 78);
            this.sendBtn.TabIndex = 10;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = false;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // adminprofile
            // 
            this.adminprofile.Image = global::try_messaging.Properties.Resources.posangBoang;
            this.adminprofile.Location = new System.Drawing.Point(3, 4);
            this.adminprofile.Name = "adminprofile";
            this.adminprofile.Size = new System.Drawing.Size(51, 45);
            this.adminprofile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.adminprofile.TabIndex = 15;
            this.adminprofile.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(271, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Boardinging House Management";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(60, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 18);
            this.label3.TabIndex = 17;
            this.label3.Text = "Admin";
            // 
            // tenantcomform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(628, 425);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.adminprofile);
            this.Controls.Add(this.conversationBox);
            this.Controls.Add(this.typeMessage);
            this.Controls.Add(this.sendBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "tenantcomform";
            this.Text = "Messenger";
            this.Load += new System.EventHandler(this.tenantcomform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.adminprofile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox conversationBox;
        private System.Windows.Forms.RichTextBox typeMessage;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.PictureBox adminprofile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}