namespace try_messaging
{
    partial class tenant_dashboard
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
            this.changepassBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // changepassBtn
            // 
            this.changepassBtn.Location = new System.Drawing.Point(58, 185);
            this.changepassBtn.Name = "changepassBtn";
            this.changepassBtn.Size = new System.Drawing.Size(87, 87);
            this.changepassBtn.TabIndex = 1;
            this.changepassBtn.Text = "change password";
            this.changepassBtn.UseVisualStyleBackColor = true;
            this.changepassBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(525, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "welcome tenant!";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(58, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 87);
            this.button1.TabIndex = 3;
            this.button1.Text = "Message admin";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(186, 64);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(840, 524);
            this.displayPanel.TabIndex = 4;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            // 
            // tenant_dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 611);
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.changepassBtn);
            this.Name = "tenant_dashboard";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button changepassBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel displayPanel;
    }
}