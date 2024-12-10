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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label14 = new System.Windows.Forms.Label();
            this.tenantpaymentsTable = new System.Windows.Forms.DataGridView();
            this.paymentLogs = new System.Windows.Forms.DataGridView();
            this.label16 = new System.Windows.Forms.Label();
            this.accept_Btn = new System.Windows.Forms.Button();
            this.decline_Btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.sortCombo = new System.Windows.Forms.ComboBox();
            this.searchBar = new System.Windows.Forms.TextBox();
            this.billStatusTable = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.refresh_Btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tenantpaymentsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paymentLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.billStatusTable)).BeginInit();
            this.SuspendLayout();
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(148, 20);
            this.label14.TabIndex = 90;
            this.label14.Text = "Tenant Payments";
            // 
            // tenantpaymentsTable
            // 
            this.tenantpaymentsTable.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tenantpaymentsTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tenantpaymentsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(65)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tenantpaymentsTable.DefaultCellStyle = dataGridViewCellStyle2;
            this.tenantpaymentsTable.Location = new System.Drawing.Point(4, 32);
            this.tenantpaymentsTable.Name = "tenantpaymentsTable";
            this.tenantpaymentsTable.ReadOnly = true;
            this.tenantpaymentsTable.RowHeadersVisible = false;
            this.tenantpaymentsTable.Size = new System.Drawing.Size(861, 214);
            this.tenantpaymentsTable.TabIndex = 91;
            this.tenantpaymentsTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tenantpaymentsTable_CellContentClick);
            // 
            // paymentLogs
            // 
            this.paymentLogs.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.paymentLogs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.paymentLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.paymentLogs.DefaultCellStyle = dataGridViewCellStyle4;
            this.paymentLogs.Location = new System.Drawing.Point(343, 340);
            this.paymentLogs.Name = "paymentLogs";
            this.paymentLogs.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.paymentLogs.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.paymentLogs.RowHeadersVisible = false;
            this.paymentLogs.Size = new System.Drawing.Size(522, 187);
            this.paymentLogs.TabIndex = 92;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 284);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(136, 20);
            this.label16.TabIndex = 93;
            this.label16.Text = "Payment Status";
            // 
            // accept_Btn
            // 
            this.accept_Btn.BackColor = System.Drawing.Color.White;
            this.accept_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.accept_Btn.Location = new System.Drawing.Point(85, 252);
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
            this.decline_Btn.Location = new System.Drawing.Point(4, 252);
            this.decline_Btn.Name = "decline_Btn";
            this.decline_Btn.Size = new System.Drawing.Size(72, 23);
            this.decline_Btn.TabIndex = 119;
            this.decline_Btn.Text = "Decline";
            this.decline_Btn.UseVisualStyleBackColor = false;
            this.decline_Btn.Click += new System.EventHandler(this.decline_Btn_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(740, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 23);
            this.button1.TabIndex = 118;
            this.button1.Text = "Issue New Bill";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(570, 314);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 123;
            this.label2.Text = "sort by:";
            // 
            // sortCombo
            // 
            this.sortCombo.FormattingEnabled = true;
            this.sortCombo.Location = new System.Drawing.Point(636, 313);
            this.sortCombo.Name = "sortCombo";
            this.sortCombo.Size = new System.Drawing.Size(121, 21);
            this.sortCombo.TabIndex = 122;
            this.sortCombo.SelectedIndexChanged += new System.EventHandler(this.sortCombo_SelectedIndexChanged);
            // 
            // searchBar
            // 
            this.searchBar.Location = new System.Drawing.Point(343, 314);
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(196, 20);
            this.searchBar.TabIndex = 120;
            this.searchBar.TextChanged += new System.EventHandler(this.searchBar_TextChanged);
            // 
            // billStatusTable
            // 
            this.billStatusTable.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.billStatusTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.billStatusTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.billStatusTable.DefaultCellStyle = dataGridViewCellStyle7;
            this.billStatusTable.Location = new System.Drawing.Point(4, 307);
            this.billStatusTable.Name = "billStatusTable";
            this.billStatusTable.ReadOnly = true;
            this.billStatusTable.RowHeadersVisible = false;
            this.billStatusTable.Size = new System.Drawing.Size(333, 220);
            this.billStatusTable.TabIndex = 125;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(339, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
            this.label1.TabIndex = 126;
            this.label1.Text = "Payment Details";
            // 
            // refresh_Btn
            // 
            this.refresh_Btn.BackColor = System.Drawing.Color.White;
            this.refresh_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refresh_Btn.Location = new System.Drawing.Point(4, 530);
            this.refresh_Btn.Name = "refresh_Btn";
            this.refresh_Btn.Size = new System.Drawing.Size(72, 23);
            this.refresh_Btn.TabIndex = 127;
            this.refresh_Btn.Text = "Refresh";
            this.refresh_Btn.UseVisualStyleBackColor = false;
            this.refresh_Btn.Click += new System.EventHandler(this.refresh_Btn_Click);
            // 
            // payment_admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(869, 556);
            this.Controls.Add(this.refresh_Btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.billStatusTable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sortCombo);
            this.Controls.Add(this.searchBar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.decline_Btn);
            this.Controls.Add(this.accept_Btn);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.paymentLogs);
            this.Controls.Add(this.tenantpaymentsTable);
            this.Controls.Add(this.label14);
            this.Name = "payment_admin";
            this.Text = "payment_admin";
            this.Load += new System.EventHandler(this.payment_admin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tenantpaymentsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paymentLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.billStatusTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView tenantpaymentsTable;
        private System.Windows.Forms.DataGridView paymentLogs;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button accept_Btn;
        private System.Windows.Forms.Button decline_Btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sortCombo;
        private System.Windows.Forms.TextBox searchBar;
        private System.Windows.Forms.DataGridView billStatusTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button refresh_Btn;
    }
}