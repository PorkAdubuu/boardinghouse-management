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
            this.conversationBox = new System.Windows.Forms.RichTextBox();
            this.typeMessage = new System.Windows.Forms.RichTextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // conversationBox
            // 
            this.conversationBox.BackColor = System.Drawing.Color.White;
            this.conversationBox.Location = new System.Drawing.Point(199, 124);
            this.conversationBox.Name = "conversationBox";
            this.conversationBox.ReadOnly = true;
            this.conversationBox.Size = new System.Drawing.Size(319, 183);
            this.conversationBox.TabIndex = 12;
            this.conversationBox.Text = "";
            this.conversationBox.TextChanged += new System.EventHandler(this.conversationBox_TextChanged);
            // 
            // typeMessage
            // 
            this.typeMessage.Location = new System.Drawing.Point(199, 339);
            this.typeMessage.Name = "typeMessage";
            this.typeMessage.Size = new System.Drawing.Size(319, 50);
            this.typeMessage.TabIndex = 11;
            this.typeMessage.Text = "";
            this.typeMessage.TextChanged += new System.EventHandler(this.typeMessage_TextChanged);
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(535, 339);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(49, 23);
            this.sendBtn.TabIndex = 10;
            this.sendBtn.Text = "send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(278, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Messenger-tenant";
            // 
            // tenantcomform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.conversationBox);
            this.Controls.Add(this.typeMessage);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.label1);
            this.Name = "tenantcomform";
            this.Text = "tenantcomform";
            this.Load += new System.EventHandler(this.tenantcomform_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox conversationBox;
        private System.Windows.Forms.RichTextBox typeMessage;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Label label1;
    }
}