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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.sendBtn = new System.Windows.Forms.Button();
            this.typeMessage = new System.Windows.Forms.RichTextBox();
            this.conversationBox = new System.Windows.Forms.RichTextBox();
            this.tenantlistsGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.tenantlistsGrid)).BeginInit();
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
            this.sendBtn.Location = new System.Drawing.Point(639, 308);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(49, 23);
            this.sendBtn.TabIndex = 2;
            this.sendBtn.Text = "send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // typeMessage
            // 
            this.typeMessage.BackColor = System.Drawing.Color.SeaShell;
            this.typeMessage.Location = new System.Drawing.Point(314, 308);
            this.typeMessage.Name = "typeMessage";
            this.typeMessage.Size = new System.Drawing.Size(319, 50);
            this.typeMessage.TabIndex = 3;
            this.typeMessage.Text = "";
            this.typeMessage.TextChanged += new System.EventHandler(this.typeMessage_TextChanged);
            // 
            // conversationBox
            // 
            this.conversationBox.BackColor = System.Drawing.Color.SeaShell;
            this.conversationBox.Location = new System.Drawing.Point(314, 106);
            this.conversationBox.Name = "conversationBox";
            this.conversationBox.ReadOnly = true;
            this.conversationBox.Size = new System.Drawing.Size(319, 183);
            this.conversationBox.TabIndex = 4;
            this.conversationBox.Text = "";
            this.conversationBox.TextChanged += new System.EventHandler(this.conversationBox_TextChanged);
            // 
            // tenantlistsGrid
            // 
            this.tenantlistsGrid.AllowUserToAddRows = false;
            this.tenantlistsGrid.AllowUserToDeleteRows = false;
            this.tenantlistsGrid.AllowUserToResizeColumns = false;
            this.tenantlistsGrid.AllowUserToResizeRows = false;
            this.tenantlistsGrid.BackgroundColor = System.Drawing.Color.SeaShell;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tenantlistsGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tenantlistsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tenantlistsGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tenantlistsGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.tenantlistsGrid.EnableHeadersVisualStyles = false;
            this.tenantlistsGrid.GridColor = System.Drawing.Color.White;
            this.tenantlistsGrid.Location = new System.Drawing.Point(119, 106);
            this.tenantlistsGrid.MultiSelect = false;
            this.tenantlistsGrid.Name = "tenantlistsGrid";
            this.tenantlistsGrid.ReadOnly = true;
            this.tenantlistsGrid.RowHeadersVisible = false;
            this.tenantlistsGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.tenantlistsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tenantlistsGrid.Size = new System.Drawing.Size(143, 252);
            this.tenantlistsGrid.StandardTab = true;
            this.tenantlistsGrid.TabIndex = 11;
            this.tenantlistsGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tenantlistsGrid_CellContentClick);
            // 
            // admincomform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tenantlistsGrid);
            this.Controls.Add(this.conversationBox);
            this.Controls.Add(this.typeMessage);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.label1);
            this.Name = "admincomform";
            this.Text = "admincomform";
            this.Load += new System.EventHandler(this.admincomform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tenantlistsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.RichTextBox typeMessage;
        private System.Windows.Forms.RichTextBox conversationBox;
        private System.Windows.Forms.DataGridView tenantlistsGrid;
    }
}