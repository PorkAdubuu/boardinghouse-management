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
            this.sendBtn = new System.Windows.Forms.Button();
            this.typeMessage = new System.Windows.Forms.RichTextBox();
            this.conversationBox = new System.Windows.Forms.RichTextBox();
            this.tenantlistsGrid = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.tenantName = new System.Windows.Forms.Label();
            this.tenantProfile = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.tenantlistsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tenantProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(794, 490);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(57, 78);
            this.sendBtn.TabIndex = 2;
            this.sendBtn.Text = "send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // typeMessage
            // 
            this.typeMessage.BackColor = System.Drawing.Color.White;
            this.typeMessage.Location = new System.Drawing.Point(226, 490);
            this.typeMessage.Name = "typeMessage";
            this.typeMessage.Size = new System.Drawing.Size(562, 78);
            this.typeMessage.TabIndex = 3;
            this.typeMessage.Text = "";
            this.typeMessage.TextChanged += new System.EventHandler(this.typeMessage_TextChanged);
            // 
            // conversationBox
            // 
            this.conversationBox.BackColor = System.Drawing.Color.White;
            this.conversationBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conversationBox.Location = new System.Drawing.Point(226, 55);
            this.conversationBox.Name = "conversationBox";
            this.conversationBox.ReadOnly = true;
            this.conversationBox.Size = new System.Drawing.Size(625, 429);
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
            this.tenantlistsGrid.BackgroundColor = System.Drawing.Color.White;
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
            this.tenantlistsGrid.Location = new System.Drawing.Point(1, 33);
            this.tenantlistsGrid.MultiSelect = false;
            this.tenantlistsGrid.Name = "tenantlistsGrid";
            this.tenantlistsGrid.ReadOnly = true;
            this.tenantlistsGrid.RowHeadersVisible = false;
            this.tenantlistsGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.tenantlistsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tenantlistsGrid.Size = new System.Drawing.Size(219, 535);
            this.tenantlistsGrid.StandardTab = true;
            this.tenantlistsGrid.TabIndex = 11;
            this.tenantlistsGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tenantlistsGrid_CellContentClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(283, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 18);
            this.label3.TabIndex = 20;
            this.label3.Text = "Tenant";
            // 
            // tenantName
            // 
            this.tenantName.AutoSize = true;
            this.tenantName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tenantName.Location = new System.Drawing.Point(283, 6);
            this.tenantName.Name = "tenantName";
            this.tenantName.Size = new System.Drawing.Size(121, 20);
            this.tenantName.TabIndex = 19;
            this.tenantName.Text = "Tenant, Name";
            // 
            // tenantProfile
            // 
            this.tenantProfile.Image = global::try_messaging.Properties.Resources.DefaultProfile;
            this.tenantProfile.Location = new System.Drawing.Point(226, 6);
            this.tenantProfile.Name = "tenantProfile";
            this.tenantProfile.Size = new System.Drawing.Size(51, 45);
            this.tenantProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.tenantProfile.TabIndex = 18;
            this.tenantProfile.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(92, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(94, 24);
            this.textBox1.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Room/name";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Image = global::try_messaging.Properties.Resources.magnifying_glass__1_;
            this.pictureBox1.Location = new System.Drawing.Point(192, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(1, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 41);
            this.panel1.TabIndex = 24;
            // 
            // admincomform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(869, 574);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tenantName);
            this.Controls.Add(this.tenantProfile);
            this.Controls.Add(this.tenantlistsGrid);
            this.Controls.Add(this.conversationBox);
            this.Controls.Add(this.typeMessage);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.panel1);
            this.Name = "admincomform";
            this.Text = "admincomform";
            this.Load += new System.EventHandler(this.admincomform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tenantlistsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tenantProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.RichTextBox typeMessage;
        private System.Windows.Forms.RichTextBox conversationBox;
        private System.Windows.Forms.DataGridView tenantlistsGrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label tenantName;
        private System.Windows.Forms.PictureBox tenantProfile;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
    }
}