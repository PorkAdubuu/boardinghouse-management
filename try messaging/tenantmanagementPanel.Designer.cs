namespace try_messaging
{
    partial class tenantmanagementPanel
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
            this.manageSelection = new System.Windows.Forms.ComboBox();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // manageSelection
            // 
            this.manageSelection.FormattingEnabled = true;
            this.manageSelection.Items.AddRange(new object[] {
            "Add new tenant",
            "Tenant lists"});
            this.manageSelection.Location = new System.Drawing.Point(7, 0);
            this.manageSelection.Name = "manageSelection";
            this.manageSelection.Size = new System.Drawing.Size(289, 21);
            this.manageSelection.TabIndex = 84;
            this.manageSelection.SelectedIndexChanged += new System.EventHandler(this.manageSelection_SelectedIndexChanged);
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(-1, 38);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(868, 538);
            this.displayPanel.TabIndex = 85;
            // 
            // tenantmanagementPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(869, 578);
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.manageSelection);
            this.Name = "tenantmanagementPanel";
            this.Text = "tenantmanagementPanel";
            this.Load += new System.EventHandler(this.tenantmanagementPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox manageSelection;
        private System.Windows.Forms.Panel displayPanel;
    }
}