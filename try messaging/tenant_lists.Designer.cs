namespace try_messaging
{
    partial class tenant_lists
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
            this.searchBar = new System.Windows.Forms.TextBox();
            this.search_Btn = new System.Windows.Forms.Button();
            this.sortCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.refresh_Btn = new System.Windows.Forms.Button();
            this.delete_Btn = new System.Windows.Forms.Button();
            this.tenantList = new System.Windows.Forms.DataGridView();
            this.export_Btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.houseCombo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.tenantList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 66;
            this.label1.Text = "Tenant lists";
            // 
            // searchBar
            // 
            this.searchBar.Location = new System.Drawing.Point(6, 32);
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(196, 20);
            this.searchBar.TabIndex = 68;
            this.searchBar.Enter += new System.EventHandler(this.searchBar_Enter);
            this.searchBar.Leave += new System.EventHandler(this.searchBar_Leave);
            // 
            // search_Btn
            // 
            this.search_Btn.BackColor = System.Drawing.Color.White;
            this.search_Btn.Location = new System.Drawing.Point(208, 30);
            this.search_Btn.Name = "search_Btn";
            this.search_Btn.Size = new System.Drawing.Size(75, 23);
            this.search_Btn.TabIndex = 69;
            this.search_Btn.Text = "Search";
            this.search_Btn.UseVisualStyleBackColor = false;
            this.search_Btn.Click += new System.EventHandler(this.search_Btn_Click);
            // 
            // sortCombo
            // 
            this.sortCombo.FormattingEnabled = true;
            this.sortCombo.Location = new System.Drawing.Point(379, 32);
            this.sortCombo.Name = "sortCombo";
            this.sortCombo.Size = new System.Drawing.Size(121, 21);
            this.sortCombo.TabIndex = 70;
            this.sortCombo.SelectedIndexChanged += new System.EventHandler(this.sortCombo_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(313, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 71;
            this.label2.Text = "sort by:";
            // 
            // refresh_Btn
            // 
            this.refresh_Btn.BackColor = System.Drawing.Color.White;
            this.refresh_Btn.Location = new System.Drawing.Point(87, 506);
            this.refresh_Btn.Name = "refresh_Btn";
            this.refresh_Btn.Size = new System.Drawing.Size(75, 23);
            this.refresh_Btn.TabIndex = 72;
            this.refresh_Btn.Text = "Refresh";
            this.refresh_Btn.UseVisualStyleBackColor = false;
            this.refresh_Btn.Click += new System.EventHandler(this.refresh_Btn_Click);
            // 
            // delete_Btn
            // 
            this.delete_Btn.BackColor = System.Drawing.Color.White;
            this.delete_Btn.Location = new System.Drawing.Point(6, 506);
            this.delete_Btn.Name = "delete_Btn";
            this.delete_Btn.Size = new System.Drawing.Size(75, 23);
            this.delete_Btn.TabIndex = 73;
            this.delete_Btn.Text = "Delete";
            this.delete_Btn.UseVisualStyleBackColor = false;
            this.delete_Btn.Click += new System.EventHandler(this.delete_Btn_Click);
            // 
            // tenantList
            // 
            this.tenantList.BackgroundColor = System.Drawing.Color.White;
            this.tenantList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tenantList.Location = new System.Drawing.Point(6, 59);
            this.tenantList.Name = "tenantList";
            this.tenantList.ReadOnly = true;
            this.tenantList.RowHeadersVisible = false;
            this.tenantList.Size = new System.Drawing.Size(861, 441);
            this.tenantList.TabIndex = 74;
            this.tenantList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tenantList_CellClick);
            // 
            // export_Btn
            // 
            this.export_Btn.BackColor = System.Drawing.Color.White;
            this.export_Btn.Location = new System.Drawing.Point(785, 506);
            this.export_Btn.Name = "export_Btn";
            this.export_Btn.Size = new System.Drawing.Size(75, 23);
            this.export_Btn.TabIndex = 75;
            this.export_Btn.Text = "Export";
            this.export_Btn.UseVisualStyleBackColor = false;
            this.export_Btn.Click += new System.EventHandler(this.export_Btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(545, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 77;
            this.label3.Text = "House:";
            // 
            // houseCombo
            // 
            this.houseCombo.FormattingEnabled = true;
            this.houseCombo.Location = new System.Drawing.Point(611, 31);
            this.houseCombo.Name = "houseCombo";
            this.houseCombo.Size = new System.Drawing.Size(121, 21);
            this.houseCombo.TabIndex = 76;
            this.houseCombo.SelectedIndexChanged += new System.EventHandler(this.houseCombo_SelectedIndexChanged);
            // 
            // tenant_lists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(869, 578);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.houseCombo);
            this.Controls.Add(this.export_Btn);
            this.Controls.Add(this.tenantList);
            this.Controls.Add(this.delete_Btn);
            this.Controls.Add(this.refresh_Btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sortCombo);
            this.Controls.Add(this.search_Btn);
            this.Controls.Add(this.searchBar);
            this.Controls.Add(this.label1);
            this.Name = "tenant_lists";
            this.Text = "tenant_lists";
            this.Load += new System.EventHandler(this.tenant_lists_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tenantList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchBar;
        private System.Windows.Forms.Button search_Btn;
        private System.Windows.Forms.ComboBox sortCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button delete_Btn;
        private System.Windows.Forms.DataGridView tenantList;
        private System.Windows.Forms.Button refresh_Btn;
        private System.Windows.Forms.Button export_Btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox houseCombo;
    }
}