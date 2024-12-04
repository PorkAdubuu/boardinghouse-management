namespace try_messaging
{
    partial class boardining_houses
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
            this.export_Btn = new System.Windows.Forms.Button();
            this.houseList = new System.Windows.Forms.DataGridView();
            this.delete_Btn = new System.Windows.Forms.Button();
            this.refresh_Btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.sortCombo = new System.Windows.Forms.ComboBox();
            this.search_Btn = new System.Windows.Forms.Button();
            this.searchBar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.houseNameText = new System.Windows.Forms.TextBox();
            this.addressText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.capacityText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.confirm_Btn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.policy_Btn = new System.Windows.Forms.Button();
            this.image_Btn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.image_preview = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.policyFileLabel = new System.Windows.Forms.RichTextBox();
            this.houseNumText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.houseList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.image_preview)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // export_Btn
            // 
            this.export_Btn.BackColor = System.Drawing.Color.White;
            this.export_Btn.Location = new System.Drawing.Point(792, 284);
            this.export_Btn.Name = "export_Btn";
            this.export_Btn.Size = new System.Drawing.Size(75, 23);
            this.export_Btn.TabIndex = 84;
            this.export_Btn.Text = "Export";
            this.export_Btn.UseVisualStyleBackColor = false;
            this.export_Btn.Click += new System.EventHandler(this.export_Btn_Click);
            // 
            // houseList
            // 
            this.houseList.BackgroundColor = System.Drawing.Color.White;
            this.houseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.houseList.Location = new System.Drawing.Point(6, 59);
            this.houseList.Name = "houseList";
            this.houseList.ReadOnly = true;
            this.houseList.RowHeadersVisible = false;
            this.houseList.Size = new System.Drawing.Size(861, 219);
            this.houseList.TabIndex = 83;
            this.houseList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.houseList_CellClick);
            // 
            // delete_Btn
            // 
            this.delete_Btn.BackColor = System.Drawing.Color.White;
            this.delete_Btn.Location = new System.Drawing.Point(9, 284);
            this.delete_Btn.Name = "delete_Btn";
            this.delete_Btn.Size = new System.Drawing.Size(75, 23);
            this.delete_Btn.TabIndex = 82;
            this.delete_Btn.Text = "Delete";
            this.delete_Btn.UseVisualStyleBackColor = false;
            this.delete_Btn.Click += new System.EventHandler(this.delete_Btn_Click);
            // 
            // refresh_Btn
            // 
            this.refresh_Btn.BackColor = System.Drawing.Color.White;
            this.refresh_Btn.Location = new System.Drawing.Point(90, 284);
            this.refresh_Btn.Name = "refresh_Btn";
            this.refresh_Btn.Size = new System.Drawing.Size(75, 23);
            this.refresh_Btn.TabIndex = 81;
            this.refresh_Btn.Text = "Refresh";
            this.refresh_Btn.UseVisualStyleBackColor = false;
            this.refresh_Btn.Click += new System.EventHandler(this.refresh_Btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(313, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 80;
            this.label2.Text = "sort by:";
            // 
            // sortCombo
            // 
            this.sortCombo.FormattingEnabled = true;
            this.sortCombo.Location = new System.Drawing.Point(379, 32);
            this.sortCombo.Name = "sortCombo";
            this.sortCombo.Size = new System.Drawing.Size(121, 21);
            this.sortCombo.TabIndex = 79;
            this.sortCombo.SelectedIndexChanged += new System.EventHandler(this.sortCombo_SelectedIndexChanged);
            // 
            // search_Btn
            // 
            this.search_Btn.BackColor = System.Drawing.Color.White;
            this.search_Btn.Location = new System.Drawing.Point(208, 30);
            this.search_Btn.Name = "search_Btn";
            this.search_Btn.Size = new System.Drawing.Size(75, 23);
            this.search_Btn.TabIndex = 78;
            this.search_Btn.Text = "Search";
            this.search_Btn.UseVisualStyleBackColor = false;
            this.search_Btn.Click += new System.EventHandler(this.search_Btn_Click);
            // 
            // searchBar
            // 
            this.searchBar.Location = new System.Drawing.Point(6, 32);
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(196, 20);
            this.searchBar.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 20);
            this.label1.TabIndex = 76;
            this.label1.Text = "Boarding Houses Lists";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 310);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 20);
            this.label3.TabIndex = 85;
            this.label3.Text = "Add boarding house";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 359);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 16);
            this.label4.TabIndex = 87;
            this.label4.Text = "House name";
            // 
            // houseNameText
            // 
            this.houseNameText.Location = new System.Drawing.Point(28, 378);
            this.houseNameText.Name = "houseNameText";
            this.houseNameText.Size = new System.Drawing.Size(164, 20);
            this.houseNameText.TabIndex = 88;
            // 
            // addressText
            // 
            this.addressText.Location = new System.Drawing.Point(22, 142);
            this.addressText.Name = "addressText";
            this.addressText.Size = new System.Drawing.Size(164, 20);
            this.addressText.TabIndex = 90;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 16);
            this.label5.TabIndex = 89;
            this.label5.Text = "Address";
            // 
            // capacityText
            // 
            this.capacityText.Location = new System.Drawing.Point(22, 193);
            this.capacityText.Name = "capacityText";
            this.capacityText.Size = new System.Drawing.Size(164, 20);
            this.capacityText.TabIndex = 92;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 16);
            this.label6.TabIndex = 91;
            this.label6.Text = "Capacity";
            // 
            // confirm_Btn
            // 
            this.confirm_Btn.BackColor = System.Drawing.Color.White;
            this.confirm_Btn.Location = new System.Drawing.Point(287, 209);
            this.confirm_Btn.Name = "confirm_Btn";
            this.confirm_Btn.Size = new System.Drawing.Size(162, 23);
            this.confirm_Btn.TabIndex = 94;
            this.confirm_Btn.Text = "Confirm";
            this.confirm_Btn.UseVisualStyleBackColor = false;
            this.confirm_Btn.Click += new System.EventHandler(this.confirm_Btn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(261, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 16);
            this.label7.TabIndex = 97;
            this.label7.Text = "Policies";
            // 
            // policy_Btn
            // 
            this.policy_Btn.BackColor = System.Drawing.Color.White;
            this.policy_Btn.Location = new System.Drawing.Point(264, 44);
            this.policy_Btn.Name = "policy_Btn";
            this.policy_Btn.Size = new System.Drawing.Size(75, 23);
            this.policy_Btn.TabIndex = 98;
            this.policy_Btn.Text = "Upload";
            this.policy_Btn.UseVisualStyleBackColor = false;
            this.policy_Btn.Click += new System.EventHandler(this.policy_Btn_Click);
            // 
            // image_Btn
            // 
            this.image_Btn.BackColor = System.Drawing.Color.White;
            this.image_Btn.Location = new System.Drawing.Point(264, 123);
            this.image_Btn.Name = "image_Btn";
            this.image_Btn.Size = new System.Drawing.Size(75, 23);
            this.image_Btn.TabIndex = 100;
            this.image_Btn.Text = "Upload";
            this.image_Btn.UseVisualStyleBackColor = false;
            this.image_Btn.Click += new System.EventHandler(this.image_Btn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(261, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 16);
            this.label8.TabIndex = 99;
            this.label8.Text = "Image";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog1";
            // 
            // image_preview
            // 
            this.image_preview.BackColor = System.Drawing.Color.White;
            this.image_preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.image_preview.Location = new System.Drawing.Point(493, 15);
            this.image_preview.Name = "image_preview";
            this.image_preview.Size = new System.Drawing.Size(358, 188);
            this.image_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image_preview.TabIndex = 101;
            this.image_preview.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.policyFileLabel);
            this.panel1.Controls.Add(this.houseNumText);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.capacityText);
            this.panel1.Controls.Add(this.confirm_Btn);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.image_preview);
            this.panel1.Controls.Add(this.addressText);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.policy_Btn);
            this.panel1.Controls.Add(this.image_Btn);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(6, 334);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(861, 240);
            this.panel1.TabIndex = 102;
            // 
            // policyFileLabel
            // 
            this.policyFileLabel.BackColor = System.Drawing.Color.White;
            this.policyFileLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.policyFileLabel.Location = new System.Drawing.Point(264, 70);
            this.policyFileLabel.Name = "policyFileLabel";
            this.policyFileLabel.ReadOnly = true;
            this.policyFileLabel.Size = new System.Drawing.Size(223, 29);
            this.policyFileLabel.TabIndex = 106;
            this.policyFileLabel.Text = "";
            // 
            // houseNumText
            // 
            this.houseNumText.Location = new System.Drawing.Point(22, 93);
            this.houseNumText.Name = "houseNumText";
            this.houseNumText.Size = new System.Drawing.Size(164, 20);
            this.houseNumText.TabIndex = 104;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(19, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 16);
            this.label9.TabIndex = 103;
            this.label9.Text = "House number";
            // 
            // boardining_houses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(869, 578);
            this.Controls.Add(this.houseNameText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.export_Btn);
            this.Controls.Add(this.houseList);
            this.Controls.Add(this.delete_Btn);
            this.Controls.Add(this.refresh_Btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sortCombo);
            this.Controls.Add(this.search_Btn);
            this.Controls.Add(this.searchBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "boardining_houses";
            this.Text = "boardining_houses";
            this.Load += new System.EventHandler(this.boardining_houses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.houseList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.image_preview)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button export_Btn;
        private System.Windows.Forms.DataGridView houseList;
        private System.Windows.Forms.Button delete_Btn;
        private System.Windows.Forms.Button refresh_Btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sortCombo;
        private System.Windows.Forms.Button search_Btn;
        private System.Windows.Forms.TextBox searchBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox houseNameText;
        private System.Windows.Forms.TextBox addressText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox capacityText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button confirm_Btn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button policy_Btn;
        private System.Windows.Forms.Button image_Btn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.PictureBox image_preview;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox houseNumText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox policyFileLabel;
    }
}