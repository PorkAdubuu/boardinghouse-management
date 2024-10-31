namespace try_messaging
{
    partial class admincomform
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
            this.label1 = new System.Windows.Forms.Label();
            this.sendBtn = new System.Windows.Forms.Button();
            this.typeMessage = new System.Windows.Forms.RichTextBox();
            this.conversationBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.messagesentlabel = new System.Windows.Forms.Label();
            this.tenantlistCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(309, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Messenger-admin";
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(702, 321);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(49, 23);
            this.sendBtn.TabIndex = 2;
            this.sendBtn.Text = "send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // typeMessage
            // 
            this.typeMessage.Location = new System.Drawing.Point(377, 321);
            this.typeMessage.Name = "typeMessage";
            this.typeMessage.Size = new System.Drawing.Size(319, 50);
            this.typeMessage.TabIndex = 3;
            this.typeMessage.Text = "";
            this.typeMessage.TextChanged += new System.EventHandler(this.typeMessage_TextChanged);
            // 
            // conversationBox
            // 
            this.conversationBox.Location = new System.Drawing.Point(377, 106);
            this.conversationBox.Name = "conversationBox";
            this.conversationBox.ReadOnly = true;
            this.conversationBox.Size = new System.Drawing.Size(319, 183);
            this.conversationBox.TabIndex = 4;
            this.conversationBox.Text = "";
            this.conversationBox.TextChanged += new System.EventHandler(this.conversationBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "tenant lists";
            // 
            // messagesentlabel
            // 
            this.messagesentlabel.AutoSize = true;
            this.messagesentlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messagesentlabel.Location = new System.Drawing.Point(374, 302);
            this.messagesentlabel.Name = "messagesentlabel";
            this.messagesentlabel.Size = new System.Drawing.Size(44, 16);
            this.messagesentlabel.TabIndex = 7;
            this.messagesentlabel.Text = "label3";
            this.messagesentlabel.Click += new System.EventHandler(this.messagesentlabel_Click);
            // 
            // tenantlistCombo
            // 
            this.tenantlistCombo.FormattingEnabled = true;
            this.tenantlistCombo.Location = new System.Drawing.Point(73, 135);
            this.tenantlistCombo.Name = "tenantlistCombo";
            this.tenantlistCombo.Size = new System.Drawing.Size(159, 21);
            this.tenantlistCombo.TabIndex = 8;
            this.tenantlistCombo.SelectedIndexChanged += new System.EventHandler(this.tenantlistCombo_SelectedIndexChanged);
            // 
            // admincomform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tenantlistCombo);
            this.Controls.Add(this.messagesentlabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.conversationBox);
            this.Controls.Add(this.typeMessage);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.label1);
            this.Name = "admincomform";
            this.Text = "admincomform";
            this.Load += new System.EventHandler(this.admincomform_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.RichTextBox typeMessage;
        private System.Windows.Forms.RichTextBox conversationBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label messagesentlabel;
        private System.Windows.Forms.ComboBox tenantlistCombo;
    }
}