﻿namespace try_messaging
{
    partial class bill_report
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nopaymentCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.overdueCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.paidCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.billCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.houseCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.export_Btn = new System.Windows.Forms.Button();
            this.billsDataGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.statusCombo = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.billsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(33)))), ((int)(((byte)(116)))));
            this.panel1.Controls.Add(this.nopaymentCount);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.overdueCount);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.paidCount);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.billCount);
            this.panel1.Controls.Add(this.label4);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(5, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(863, 74);
            this.panel1.TabIndex = 92;
            // 
            // nopaymentCount
            // 
            this.nopaymentCount.AutoSize = true;
            this.nopaymentCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nopaymentCount.Location = new System.Drawing.Point(572, 27);
            this.nopaymentCount.Name = "nopaymentCount";
            this.nopaymentCount.Size = new System.Drawing.Size(21, 24);
            this.nopaymentCount.TabIndex = 88;
            this.nopaymentCount.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(438, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 24);
            this.label6.TabIndex = 87;
            this.label6.Text = "No payment:";
            // 
            // overdueCount
            // 
            this.overdueCount.AutoSize = true;
            this.overdueCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overdueCount.Location = new System.Drawing.Point(773, 27);
            this.overdueCount.Name = "overdueCount";
            this.overdueCount.Size = new System.Drawing.Size(21, 24);
            this.overdueCount.TabIndex = 86;
            this.overdueCount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(670, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 24);
            this.label7.TabIndex = 85;
            this.label7.Text = "Overdue:";
            // 
            // paidCount
            // 
            this.paidCount.AutoSize = true;
            this.paidCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paidCount.Location = new System.Drawing.Point(336, 27);
            this.paidCount.Name = "paidCount";
            this.paidCount.Size = new System.Drawing.Size(21, 24);
            this.paidCount.TabIndex = 84;
            this.paidCount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(273, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 24);
            this.label5.TabIndex = 83;
            this.label5.Text = "Paid:";
            // 
            // billCount
            // 
            this.billCount.AutoSize = true;
            this.billCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.billCount.Location = new System.Drawing.Point(165, 27);
            this.billCount.Name = "billCount";
            this.billCount.Size = new System.Drawing.Size(21, 24);
            this.billCount.TabIndex = 82;
            this.billCount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(48, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 24);
            this.label4.TabIndex = 81;
            this.label4.Text = "Bill issued:";
            // 
            // houseCombo
            // 
            this.houseCombo.FormattingEnabled = true;
            this.houseCombo.Location = new System.Drawing.Point(157, 15);
            this.houseCombo.Name = "houseCombo";
            this.houseCombo.Size = new System.Drawing.Size(121, 21);
            this.houseCombo.TabIndex = 90;
            this.houseCombo.SelectedIndexChanged += new System.EventHandler(this.houseCombo_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 20);
            this.label3.TabIndex = 89;
            this.label3.Text = "Boarding House";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(77, 131);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(113, 20);
            this.dateTimePicker1.TabIndex = 96;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // export_Btn
            // 
            this.export_Btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(33)))), ((int)(((byte)(116)))));
            this.export_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.export_Btn.ForeColor = System.Drawing.Color.White;
            this.export_Btn.Location = new System.Drawing.Point(390, 511);
            this.export_Btn.Name = "export_Btn";
            this.export_Btn.Size = new System.Drawing.Size(75, 23);
            this.export_Btn.TabIndex = 94;
            this.export_Btn.Text = "Export";
            this.export_Btn.UseVisualStyleBackColor = false;
            this.export_Btn.Click += new System.EventHandler(this.export_Btn_Click);
            // 
            // billsDataGrid
            // 
            this.billsDataGrid.AllowUserToAddRows = false;
            this.billsDataGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(33)))), ((int)(((byte)(116)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.billsDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.billsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(33)))), ((int)(((byte)(116)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.billsDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.billsDataGrid.Location = new System.Drawing.Point(5, 156);
            this.billsDataGrid.Name = "billsDataGrid";
            this.billsDataGrid.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.billsDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.billsDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(33)))), ((int)(((byte)(116)))));
            this.billsDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.billsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.billsDataGrid.Size = new System.Drawing.Size(863, 349);
            this.billsDataGrid.TabIndex = 93;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(334, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 24);
            this.label1.TabIndex = 91;
            this.label1.Text = "Billing Summary";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 20);
            this.label8.TabIndex = 97;
            this.label8.Text = "Sort by:";
            // 
            // statusCombo
            // 
            this.statusCombo.FormattingEnabled = true;
            this.statusCombo.Items.AddRange(new object[] {
            "All",
            "No payment",
            "Paid",
            "Overdue"});
            this.statusCombo.Location = new System.Drawing.Point(196, 130);
            this.statusCombo.Name = "statusCombo";
            this.statusCombo.Size = new System.Drawing.Size(121, 21);
            this.statusCombo.TabIndex = 98;
            this.statusCombo.SelectedIndexChanged += new System.EventHandler(this.statusCombo_SelectedIndexChanged);
            // 
            // bill_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(869, 549);
            this.Controls.Add(this.statusCombo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.houseCombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.export_Btn);
            this.Controls.Add(this.billsDataGrid);
            this.Controls.Add(this.label1);
            this.Name = "bill_report";
            this.Text = "bill_report";
            this.Load += new System.EventHandler(this.bill_report_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.billsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label overdueCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label paidCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label billCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox houseCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button export_Btn;
        private System.Windows.Forms.DataGridView billsDataGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox statusCombo;
        private System.Windows.Forms.Label nopaymentCount;
        private System.Windows.Forms.Label label6;
    }
}