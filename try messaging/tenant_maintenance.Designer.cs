namespace try_messaging
{
    partial class tenant_maintenance
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
            this.trackRequests = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.typeCombo = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.descriptionTextBox1 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.uploadImageButton = new System.Windows.Forms.Button();
            this.removeImageButton = new System.Windows.Forms.Button();
            this.tenantMaintenancePanel = new System.Windows.Forms.Panel();
            this.resetFormButton = new System.Windows.Forms.Button();
            this.submitRequestButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.requestSearchBar = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackRequests)).BeginInit();
            this.tenantMaintenancePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackRequests
            // 
            this.trackRequests.AllowUserToAddRows = false;
            this.trackRequests.BackgroundColor = System.Drawing.Color.White;
            this.trackRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trackRequests.Location = new System.Drawing.Point(0, 397);
            this.trackRequests.Name = "trackRequests";
            this.trackRequests.RowHeadersVisible = false;
            this.trackRequests.Size = new System.Drawing.Size(869, 149);
            this.trackRequests.TabIndex = 75;
            this.trackRequests.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.trackRequests_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(13, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 77;
            this.label2.Text = "Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(13, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 18);
            this.label3.TabIndex = 78;
            this.label3.Text = "Description:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label4.Location = new System.Drawing.Point(14, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 18);
            this.label4.TabIndex = 79;
            this.label4.Text = "Date:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label5.Location = new System.Drawing.Point(419, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 18);
            this.label5.TabIndex = 80;
            this.label5.Text = "Upload Image:";
            // 
            // typeCombo
            // 
            this.typeCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeCombo.FormattingEnabled = true;
            this.typeCombo.Items.AddRange(new object[] {
            "Electrical",
            "Plumbing",
            "HVAC (Heating, Ventilation, and Air Conditioning)",
            "Furniture",
            "Appliances",
            "Structural",
            "Pest Control",
            "Internet/Networking",
            "Cleaning",
            "Security",
            "General Repairs"});
            this.typeCombo.Location = new System.Drawing.Point(16, 94);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(390, 26);
            this.typeCombo.TabIndex = 82;
            this.typeCombo.SelectedIndexChanged += new System.EventHandler(this.typeCombo_SelectedIndexChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(422, 94);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(324, 201);
            this.richTextBox1.TabIndex = 83;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(261, 20);
            this.label8.TabIndex = 86;
            this.label8.Text = "Submit a Maintenance Request";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label9.Location = new System.Drawing.Point(12, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 24);
            this.label9.TabIndex = 85;
            this.label9.Text = "Request Details:";
            // 
            // descriptionTextBox1
            // 
            this.descriptionTextBox1.Location = new System.Drawing.Point(17, 158);
            this.descriptionTextBox1.Multiline = true;
            this.descriptionTextBox1.Name = "descriptionTextBox1";
            this.descriptionTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.descriptionTextBox1.Size = new System.Drawing.Size(389, 83);
            this.descriptionTextBox1.TabIndex = 87;
            this.descriptionTextBox1.TextChanged += new System.EventHandler(this.descriptionTextBox1_TextChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(15, 213);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(389, 20);
            this.dateTimePicker1.TabIndex = 88;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // uploadImageButton
            // 
            this.uploadImageButton.BackColor = System.Drawing.Color.White;
            this.uploadImageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uploadImageButton.Location = new System.Drawing.Point(764, 92);
            this.uploadImageButton.Name = "uploadImageButton";
            this.uploadImageButton.Size = new System.Drawing.Size(86, 23);
            this.uploadImageButton.TabIndex = 91;
            this.uploadImageButton.Text = "Upload";
            this.uploadImageButton.UseVisualStyleBackColor = false;
            this.uploadImageButton.Click += new System.EventHandler(this.uploadImageButton_Click);
            // 
            // removeImageButton
            // 
            this.removeImageButton.BackColor = System.Drawing.Color.White;
            this.removeImageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.removeImageButton.Location = new System.Drawing.Point(764, 146);
            this.removeImageButton.Name = "removeImageButton";
            this.removeImageButton.Size = new System.Drawing.Size(86, 23);
            this.removeImageButton.TabIndex = 92;
            this.removeImageButton.Text = "Remove";
            this.removeImageButton.UseVisualStyleBackColor = false;
            this.removeImageButton.Click += new System.EventHandler(this.removeImageButton_Click);
            // 
            // tenantMaintenancePanel
            // 
            this.tenantMaintenancePanel.BackColor = System.Drawing.Color.White;
            this.tenantMaintenancePanel.Controls.Add(this.resetFormButton);
            this.tenantMaintenancePanel.Controls.Add(this.dateTimePicker1);
            this.tenantMaintenancePanel.Controls.Add(this.removeImageButton);
            this.tenantMaintenancePanel.Controls.Add(this.submitRequestButton);
            this.tenantMaintenancePanel.Controls.Add(this.uploadImageButton);
            this.tenantMaintenancePanel.Controls.Add(this.label4);
            this.tenantMaintenancePanel.Location = new System.Drawing.Point(0, 63);
            this.tenantMaintenancePanel.Name = "tenantMaintenancePanel";
            this.tenantMaintenancePanel.Size = new System.Drawing.Size(869, 289);
            this.tenantMaintenancePanel.TabIndex = 93;
            // 
            // resetFormButton
            // 
            this.resetFormButton.BackColor = System.Drawing.Color.White;
            this.resetFormButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resetFormButton.Location = new System.Drawing.Point(422, 254);
            this.resetFormButton.Name = "resetFormButton";
            this.resetFormButton.Size = new System.Drawing.Size(125, 23);
            this.resetFormButton.TabIndex = 95;
            this.resetFormButton.Text = "Reset Form";
            this.resetFormButton.UseVisualStyleBackColor = false;
            this.resetFormButton.Click += new System.EventHandler(this.resetFormButton_Click);
            // 
            // submitRequestButton
            // 
            this.submitRequestButton.BackColor = System.Drawing.Color.White;
            this.submitRequestButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitRequestButton.Location = new System.Drawing.Point(280, 254);
            this.submitRequestButton.Name = "submitRequestButton";
            this.submitRequestButton.Size = new System.Drawing.Size(125, 23);
            this.submitRequestButton.TabIndex = 94;
            this.submitRequestButton.Text = "Submit Request";
            this.submitRequestButton.UseVisualStyleBackColor = false;
            this.submitRequestButton.Click += new System.EventHandler(this.submitRequestButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(13, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 24);
            this.label1.TabIndex = 94;
            this.label1.Text = "Track Requests:";
            // 
            // requestSearchBar
            // 
            this.requestSearchBar.AcceptsReturn = true;
            this.requestSearchBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestSearchBar.Location = new System.Drawing.Point(159, 367);
            this.requestSearchBar.Name = "requestSearchBar";
            this.requestSearchBar.Size = new System.Drawing.Size(245, 24);
            this.requestSearchBar.TabIndex = 100;
            this.requestSearchBar.TextChanged += new System.EventHandler(this.requestSearchBar_TextChanged);
            this.requestSearchBar.Enter += new System.EventHandler(this.requestSearchBar_Enter);
            this.requestSearchBar.Leave += new System.EventHandler(this.requestSearchBar_Leave);
            // 
            // tenant_maintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(869, 558);
            this.Controls.Add(this.requestSearchBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.descriptionTextBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.typeCombo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackRequests);
            this.Controls.Add(this.tenantMaintenancePanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "tenant_maintenance";
            this.Text = "tenant_maintenance";
            this.Load += new System.EventHandler(this.tenant_maintenance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackRequests)).EndInit();
            this.tenantMaintenancePanel.ResumeLayout(false);
            this.tenantMaintenancePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView trackRequests;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox typeCombo;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox descriptionTextBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button uploadImageButton;
        private System.Windows.Forms.Button removeImageButton;
        private System.Windows.Forms.Panel tenantMaintenancePanel;
        private System.Windows.Forms.Button submitRequestButton;
        private System.Windows.Forms.Button resetFormButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox requestSearchBar;
    }
}