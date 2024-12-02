namespace try_messaging
{
    partial class payment_admin
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tenant_ID = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.rentBill = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.totalBIll = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.confirm_Btn = new System.Windows.Forms.Button();
            this.electricBill = new System.Windows.Forms.TextBox();
            this.waterBill = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.kWhText = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cubicText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.parkingBill = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.wifiBill = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.airconditionBill = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.startDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.roomCombo = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tenantpaymentsTable = new System.Windows.Forms.DataGridView();
            this.paymentStatus = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.accept_Btn = new System.Windows.Forms.Button();
            this.decline_Btn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tenantpaymentsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paymentStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 20);
            this.label1.TabIndex = 77;
            this.label1.Text = "Billing Statement";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tenant_ID);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.rentBill);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.totalBIll);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.confirm_Btn);
            this.panel1.Controls.Add(this.electricBill);
            this.panel1.Controls.Add(this.waterBill);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.kWhText);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.cubicText);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.parkingBill);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.wifiBill);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.airconditionBill);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.endDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.startDate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.roomCombo);
            this.panel1.Location = new System.Drawing.Point(2, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 258);
            this.panel1.TabIndex = 89;
            // 
            // tenant_ID
            // 
            this.tenant_ID.Location = new System.Drawing.Point(140, 109);
            this.tenant_ID.Name = "tenant_ID";
            this.tenant_ID.Size = new System.Drawing.Size(120, 20);
            this.tenant_ID.TabIndex = 117;
            this.tenant_ID.TextChanged += new System.EventHandler(this.tenant_ID_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(137, 87);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 18);
            this.label17.TabIndex = 116;
            this.label17.Text = "Tenant ID";
            // 
            // rentBill
            // 
            this.rentBill.Location = new System.Drawing.Point(379, 91);
            this.rentBill.Name = "rentBill";
            this.rentBill.Size = new System.Drawing.Size(81, 20);
            this.rentBill.TabIndex = 114;
            this.rentBill.Text = "3500";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(288, 93);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 18);
            this.label15.TabIndex = 113;
            this.label15.Text = "Rent";
            // 
            // totalBIll
            // 
            this.totalBIll.BackColor = System.Drawing.Color.White;
            this.totalBIll.Location = new System.Drawing.Point(291, 170);
            this.totalBIll.Name = "totalBIll";
            this.totalBIll.ReadOnly = true;
            this.totalBIll.Size = new System.Drawing.Size(118, 20);
            this.totalBIll.TabIndex = 112;
            this.totalBIll.TextChanged += new System.EventHandler(this.totalBIll_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(288, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 18);
            this.label10.TabIndex = 110;
            this.label10.Text = "Total Bill";
            // 
            // confirm_Btn
            // 
            this.confirm_Btn.BackColor = System.Drawing.Color.White;
            this.confirm_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.confirm_Btn.Location = new System.Drawing.Point(335, 218);
            this.confirm_Btn.Name = "confirm_Btn";
            this.confirm_Btn.Size = new System.Drawing.Size(125, 23);
            this.confirm_Btn.TabIndex = 90;
            this.confirm_Btn.Text = "Confirm";
            this.confirm_Btn.UseVisualStyleBackColor = false;
            this.confirm_Btn.Click += new System.EventHandler(this.confirm_Btn_Click);
            // 
            // electricBill
            // 
            this.electricBill.Location = new System.Drawing.Point(418, 62);
            this.electricBill.Name = "electricBill";
            this.electricBill.Size = new System.Drawing.Size(81, 20);
            this.electricBill.TabIndex = 109;
            // 
            // waterBill
            // 
            this.waterBill.Location = new System.Drawing.Point(418, 36);
            this.waterBill.Name = "waterBill";
            this.waterBill.Size = new System.Drawing.Size(81, 20);
            this.waterBill.TabIndex = 108;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(288, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 18);
            this.label11.TabIndex = 107;
            this.label11.Text = "Electric ";
            // 
            // kWhText
            // 
            this.kWhText.Location = new System.Drawing.Point(379, 63);
            this.kWhText.Name = "kWhText";
            this.kWhText.Size = new System.Drawing.Size(33, 20);
            this.kWhText.TabIndex = 106;
            this.kWhText.TextChanged += new System.EventHandler(this.kWhText_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(288, 39);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 18);
            this.label12.TabIndex = 105;
            this.label12.Text = "Water cubic";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(288, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 18);
            this.label13.TabIndex = 104;
            this.label13.Text = "Utilities";
            // 
            // cubicText
            // 
            this.cubicText.Location = new System.Drawing.Point(379, 37);
            this.cubicText.Name = "cubicText";
            this.cubicText.Size = new System.Drawing.Size(33, 20);
            this.cubicText.TabIndex = 103;
            this.cubicText.TextChanged += new System.EventHandler(this.cubicText_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(10, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 18);
            this.label9.TabIndex = 102;
            this.label9.Text = "Parking";
            // 
            // parkingBill
            // 
            this.parkingBill.Location = new System.Drawing.Point(101, 220);
            this.parkingBill.Name = "parkingBill";
            this.parkingBill.Size = new System.Drawing.Size(120, 20);
            this.parkingBill.TabIndex = 101;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 18);
            this.label8.TabIndex = 100;
            this.label8.Text = "Wi-Fi";
            // 
            // wifiBill
            // 
            this.wifiBill.Location = new System.Drawing.Point(101, 194);
            this.wifiBill.Name = "wifiBill";
            this.wifiBill.Size = new System.Drawing.Size(120, 20);
            this.wifiBill.TabIndex = 99;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 18);
            this.label7.TabIndex = 98;
            this.label7.Text = "Aircondition";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 18);
            this.label6.TabIndex = 97;
            this.label6.Text = "Amenities";
            // 
            // airconditionBill
            // 
            this.airconditionBill.Location = new System.Drawing.Point(101, 168);
            this.airconditionBill.Name = "airconditionBill";
            this.airconditionBill.Size = new System.Drawing.Size(120, 20);
            this.airconditionBill.TabIndex = 96;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 18);
            this.label5.TabIndex = 95;
            this.label5.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 94;
            this.label4.Text = "From";
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(60, 63);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(200, 20);
            this.endDate.TabIndex = 93;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 18);
            this.label3.TabIndex = 92;
            this.label3.Text = "Date of billing";
            // 
            // startDate
            // 
            this.startDate.Location = new System.Drawing.Point(60, 37);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(200, 20);
            this.startDate.TabIndex = 91;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 18);
            this.label2.TabIndex = 90;
            this.label2.Text = "Room number";
            // 
            // roomCombo
            // 
            this.roomCombo.FormattingEnabled = true;
            this.roomCombo.Location = new System.Drawing.Point(13, 108);
            this.roomCombo.Name = "roomCombo";
            this.roomCombo.Size = new System.Drawing.Size(121, 21);
            this.roomCombo.TabIndex = 0;
            this.roomCombo.SelectedIndexChanged += new System.EventHandler(this.roomCombo_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(512, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(148, 20);
            this.label14.TabIndex = 90;
            this.label14.Text = "Tenant Payments";
            // 
            // tenantpaymentsTable
            // 
            this.tenantpaymentsTable.BackgroundColor = System.Drawing.Color.White;
            this.tenantpaymentsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tenantpaymentsTable.Location = new System.Drawing.Point(516, 30);
            this.tenantpaymentsTable.Name = "tenantpaymentsTable";
            this.tenantpaymentsTable.RowHeadersVisible = false;
            this.tenantpaymentsTable.Size = new System.Drawing.Size(347, 249);
            this.tenantpaymentsTable.TabIndex = 91;
            this.tenantpaymentsTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tenantpaymentsTable_CellContentClick);
            // 
            // paymentStatus
            // 
            this.paymentStatus.BackgroundColor = System.Drawing.Color.White;
            this.paymentStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.paymentStatus.Location = new System.Drawing.Point(2, 314);
            this.paymentStatus.Name = "paymentStatus";
            this.paymentStatus.RowHeadersVisible = false;
            this.paymentStatus.Size = new System.Drawing.Size(861, 239);
            this.paymentStatus.TabIndex = 92;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(2, 291);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(136, 20);
            this.label16.TabIndex = 93;
            this.label16.Text = "Payment Status";
            // 
            // accept_Btn
            // 
            this.accept_Btn.BackColor = System.Drawing.Color.White;
            this.accept_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.accept_Btn.Location = new System.Drawing.Point(706, 285);
            this.accept_Btn.Name = "accept_Btn";
            this.accept_Btn.Size = new System.Drawing.Size(72, 23);
            this.accept_Btn.TabIndex = 118;
            this.accept_Btn.Text = "Accept";
            this.accept_Btn.UseVisualStyleBackColor = false;
            this.accept_Btn.Click += new System.EventHandler(this.accept_Btn_Click);
            // 
            // decline_Btn
            // 
            this.decline_Btn.BackColor = System.Drawing.Color.White;
            this.decline_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.decline_Btn.Location = new System.Drawing.Point(600, 285);
            this.decline_Btn.Name = "decline_Btn";
            this.decline_Btn.Size = new System.Drawing.Size(72, 23);
            this.decline_Btn.TabIndex = 119;
            this.decline_Btn.Text = "Decline";
            this.decline_Btn.UseVisualStyleBackColor = false;
            this.decline_Btn.Click += new System.EventHandler(this.decline_Btn_Click);
            // 
            // payment_admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(869, 556);
            this.Controls.Add(this.decline_Btn);
            this.Controls.Add(this.accept_Btn);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.paymentStatus);
            this.Controls.Add(this.tenantpaymentsTable);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "payment_admin";
            this.Text = "payment_admin";
            this.Load += new System.EventHandler(this.payment_admin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tenantpaymentsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paymentStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker startDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox roomCombo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox kWhText;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox cubicText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox parkingBill;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox wifiBill;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox airconditionBill;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox electricBill;
        private System.Windows.Forms.TextBox waterBill;
        private System.Windows.Forms.Button confirm_Btn;
        private System.Windows.Forms.TextBox rentBill;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox totalBIll;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView tenantpaymentsTable;
        private System.Windows.Forms.DataGridView paymentStatus;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tenant_ID;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button accept_Btn;
        private System.Windows.Forms.Button decline_Btn;
    }
}