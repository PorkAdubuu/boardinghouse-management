namespace try_messaging
{
    partial class LoadingDialog
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
            this.statusLabel = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(33)))), ((int)(((byte)(116)))));
            this.statusLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Location = new System.Drawing.Point(42, 37);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.ReadOnly = true;
            this.statusLabel.Size = new System.Drawing.Size(176, 55);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "";
            // 
            // LoadingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(33)))), ((int)(((byte)(116)))));
            this.ClientSize = new System.Drawing.Size(238, 103);
            this.Controls.Add(this.statusLabel);
            this.Name = "LoadingDialog";
            this.Text = "LoadingDialog";
            this.Load += new System.EventHandler(this.LoadingDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox statusLabel;
    }
}