﻿namespace try_messaging
{
    partial class admin_maintenance
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
            this.adminMaintenancePanel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.statusCombo = new System.Windows.Forms.ComboBox();
            this.typeCombo = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.searchBar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.adminMaintenancePanel2 = new System.Windows.Forms.Panel();
            this.resolvedRequests = new System.Windows.Forms.TextBox();
            this.unopenedRequests = new System.Windows.Forms.TextBox();
            this.totalRequests = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.maintenanceRequestList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openButton = new System.Windows.Forms.Button();
            this.markAsDone_Btn = new System.Windows.Forms.Button();
            this.export_Btn = new System.Windows.Forms.Button();
            this.adminMaintenancePanel1.SuspendLayout();
            this.adminMaintenancePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maintenanceRequestList)).BeginInit();
            this.SuspendLayout();
            // 
            // adminMaintenancePanel1
            // 
            this.adminMaintenancePanel1.BackColor = System.Drawing.Color.White;
            this.adminMaintenancePanel1.Controls.Add(this.label7);
            this.adminMaintenancePanel1.Controls.Add(this.statusCombo);
            this.adminMaintenancePanel1.Controls.Add(this.typeCombo);
            this.adminMaintenancePanel1.Controls.Add(this.dateTimePicker1);
            this.adminMaintenancePanel1.Controls.Add(this.searchBar);
            this.adminMaintenancePanel1.Controls.Add(this.label6);
            this.adminMaintenancePanel1.Controls.Add(this.label5);
            this.adminMaintenancePanel1.Controls.Add(this.label4);
            this.adminMaintenancePanel1.Controls.Add(this.label3);
            this.adminMaintenancePanel1.Location = new System.Drawing.Point(12, 69);
            this.adminMaintenancePanel1.Name = "adminMaintenancePanel1";
            this.adminMaintenancePanel1.Size = new System.Drawing.Size(413, 258);
            this.adminMaintenancePanel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(22, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 18);
            this.label7.TabIndex = 102;
            this.label7.Text = "Date:";
            // 
            // statusCombo
            // 
            this.statusCombo.FormattingEnabled = true;
            this.statusCombo.Location = new System.Drawing.Point(24, 155);
            this.statusCombo.Name = "statusCombo";
            this.statusCombo.Size = new System.Drawing.Size(363, 21);
            this.statusCombo.TabIndex = 101;
            this.statusCombo.SelectedIndexChanged += new System.EventHandler(this.statusCombo_SelectedIndexChanged);
            // 
            // typeCombo
            // 
            this.typeCombo.FormattingEnabled = true;
            this.typeCombo.Location = new System.Drawing.Point(25, 115);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(363, 21);
            this.typeCombo.TabIndex = 100;
            this.typeCombo.SelectedIndexChanged += new System.EventHandler(this.typeCombo_SelectedIndexChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(25, 217);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(363, 20);
            this.dateTimePicker1.TabIndex = 98;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged_1);
            // 
            // searchBar
            // 
            this.searchBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBar.Location = new System.Drawing.Point(25, 36);
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(363, 24);
            this.searchBar.TabIndex = 99;
            this.searchBar.TextChanged += new System.EventHandler(this.searchBar_TextChanged_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label6.Location = new System.Drawing.Point(22, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 18);
            this.label6.TabIndex = 95;
            this.label6.Text = "Type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label5.Location = new System.Drawing.Point(22, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 18);
            this.label5.TabIndex = 94;
            this.label5.Text = "Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label4.Location = new System.Drawing.Point(22, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 18);
            this.label4.TabIndex = 93;
            this.label4.Text = "Filter by";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(22, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 18);
            this.label3.TabIndex = 92;
            this.label3.Text = "Search:";
            // 
            // adminMaintenancePanel2
            // 
            this.adminMaintenancePanel2.BackColor = System.Drawing.Color.White;
            this.adminMaintenancePanel2.Controls.Add(this.resolvedRequests);
            this.adminMaintenancePanel2.Controls.Add(this.unopenedRequests);
            this.adminMaintenancePanel2.Controls.Add(this.totalRequests);
            this.adminMaintenancePanel2.Controls.Add(this.label13);
            this.adminMaintenancePanel2.Controls.Add(this.label10);
            this.adminMaintenancePanel2.Controls.Add(this.label11);
            this.adminMaintenancePanel2.Location = new System.Drawing.Point(446, 69);
            this.adminMaintenancePanel2.Name = "adminMaintenancePanel2";
            this.adminMaintenancePanel2.Size = new System.Drawing.Size(411, 258);
            this.adminMaintenancePanel2.TabIndex = 2;
            // 
            // resolvedRequests
            // 
            this.resolvedRequests.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resolvedRequests.Location = new System.Drawing.Point(25, 199);
            this.resolvedRequests.Name = "resolvedRequests";
            this.resolvedRequests.Size = new System.Drawing.Size(363, 24);
            this.resolvedRequests.TabIndex = 109;
            this.resolvedRequests.TextChanged += new System.EventHandler(this.resolvedRequests_TextChanged);
            // 
            // unopenedRequests
            // 
            this.unopenedRequests.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unopenedRequests.Location = new System.Drawing.Point(24, 131);
            this.unopenedRequests.Name = "unopenedRequests";
            this.unopenedRequests.Size = new System.Drawing.Size(363, 24);
            this.unopenedRequests.TabIndex = 108;
            this.unopenedRequests.TextChanged += new System.EventHandler(this.unopenedRequests_TextChanged);
            // 
            // totalRequests
            // 
            this.totalRequests.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRequests.Location = new System.Drawing.Point(24, 68);
            this.totalRequests.Name = "totalRequests";
            this.totalRequests.Size = new System.Drawing.Size(363, 24);
            this.totalRequests.TabIndex = 103;
            this.totalRequests.TextChanged += new System.EventHandler(this.totalRequests_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.White;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label13.Location = new System.Drawing.Point(22, 110);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(128, 18);
            this.label13.TabIndex = 106;
            this.label13.Text = "Pending Requests";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label10.Location = new System.Drawing.Point(22, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 18);
            this.label10.TabIndex = 103;
            this.label10.Text = "Total Requests:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.White;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label11.Location = new System.Drawing.Point(25, 178);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(141, 18);
            this.label11.TabIndex = 104;
            this.label11.Text = "Resolved Requests:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(12, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 20);
            this.label8.TabIndex = 88;
            this.label8.Text = "Maintenance";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label9.Location = new System.Drawing.Point(12, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(203, 24);
            this.label9.TabIndex = 87;
            this.label9.Text = "Maintenance Requests";
            // 
            // maintenanceRequestList
            // 
            this.maintenanceRequestList.AllowUserToAddRows = false;
            this.maintenanceRequestList.BackgroundColor = System.Drawing.Color.White;
            this.maintenanceRequestList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.maintenanceRequestList.Location = new System.Drawing.Point(12, 372);
            this.maintenanceRequestList.Name = "maintenanceRequestList";
            this.maintenanceRequestList.RowHeadersVisible = false;
            this.maintenanceRequestList.Size = new System.Drawing.Size(845, 133);
            this.maintenanceRequestList.TabIndex = 89;
            this.maintenanceRequestList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.maintenanceRequestList_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(12, 345);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 24);
            this.label1.TabIndex = 90;
            this.label1.Text = "Maintenance Request List";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(442, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 24);
            this.label2.TabIndex = 91;
            this.label2.Text = "Summary";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // openButton
            // 
            this.openButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(33)))), ((int)(((byte)(116)))));
            this.openButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openButton.ForeColor = System.Drawing.Color.White;
            this.openButton.Location = new System.Drawing.Point(12, 511);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(81, 26);
            this.openButton.TabIndex = 92;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = false;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // markAsDone_Btn
            // 
            this.markAsDone_Btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(33)))), ((int)(((byte)(116)))));
            this.markAsDone_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.markAsDone_Btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.markAsDone_Btn.ForeColor = System.Drawing.Color.White;
            this.markAsDone_Btn.Location = new System.Drawing.Point(99, 511);
            this.markAsDone_Btn.Name = "markAsDone_Btn";
            this.markAsDone_Btn.Size = new System.Drawing.Size(121, 26);
            this.markAsDone_Btn.TabIndex = 126;
            this.markAsDone_Btn.Text = "Mark as Done";
            this.markAsDone_Btn.UseVisualStyleBackColor = false;
            this.markAsDone_Btn.Click += new System.EventHandler(this.markAsDone_Btn_Click);
            // 
            // export_Btn
            // 
            this.export_Btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(33)))), ((int)(((byte)(116)))));
            this.export_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.export_Btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.export_Btn.ForeColor = System.Drawing.Color.White;
            this.export_Btn.Location = new System.Drawing.Point(763, 511);
            this.export_Btn.Name = "export_Btn";
            this.export_Btn.Size = new System.Drawing.Size(94, 26);
            this.export_Btn.TabIndex = 127;
            this.export_Btn.Text = "Export";
            this.export_Btn.UseVisualStyleBackColor = false;
            this.export_Btn.Click += new System.EventHandler(this.export_Btn_Click);
            // 
            // admin_maintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(869, 558);
            this.Controls.Add(this.export_Btn);
            this.Controls.Add(this.markAsDone_Btn);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.adminMaintenancePanel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maintenanceRequestList);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.adminMaintenancePanel1);
            this.Name = "admin_maintenance";
            this.Text = "admin_maintenance";
            this.Load += new System.EventHandler(this.admin_maintenance_Load);
            this.adminMaintenancePanel1.ResumeLayout(false);
            this.adminMaintenancePanel1.PerformLayout();
            this.adminMaintenancePanel2.ResumeLayout(false);
            this.adminMaintenancePanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maintenanceRequestList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel adminMaintenancePanel1;
        private System.Windows.Forms.Panel adminMaintenancePanel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView maintenanceRequestList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox searchBar;
        private System.Windows.Forms.ComboBox typeCombo;
        private System.Windows.Forms.ComboBox statusCombo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox resolvedRequests;
        private System.Windows.Forms.TextBox unopenedRequests;
        private System.Windows.Forms.TextBox totalRequests;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button markAsDone_Btn;
        private System.Windows.Forms.Button export_Btn;
    }
}