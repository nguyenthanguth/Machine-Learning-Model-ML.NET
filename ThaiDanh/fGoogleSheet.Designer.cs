namespace ThaiDanh
{
    partial class fGoogleSheet
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
            this.cbSheetTrangThai = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tbSheetURL = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tbSpreadSheetID = new System.Windows.Forms.TextBox();
            this.btSave = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.tbSheetEmailShare = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbSheetTrangThai
            // 
            this.cbSheetTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSheetTrangThai.FormattingEnabled = true;
            this.cbSheetTrangThai.Items.AddRange(new object[] {
            "BẬT",
            "TẮT"});
            this.cbSheetTrangThai.Location = new System.Drawing.Point(118, 12);
            this.cbSheetTrangThai.Name = "cbSheetTrangThai";
            this.cbSheetTrangThai.Size = new System.Drawing.Size(100, 21);
            this.cbSheetTrangThai.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Trạng thái";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 38);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "Sheet URL";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbSheetURL
            // 
            this.tbSheetURL.Location = new System.Drawing.Point(118, 39);
            this.tbSheetURL.Name = "tbSheetURL";
            this.tbSheetURL.Size = new System.Drawing.Size(575, 20);
            this.tbSheetURL.TabIndex = 1;
            this.tbSheetURL.Text = "Sheet URL";
            this.tbSheetURL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSheetURL.TextChanged += new System.EventHandler(this.tbSheetURL_TextChanged);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(12, 64);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 1;
            this.textBox4.Text = "SpreadSheet ID";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbSpreadSheetID
            // 
            this.tbSpreadSheetID.Location = new System.Drawing.Point(118, 65);
            this.tbSpreadSheetID.Name = "tbSpreadSheetID";
            this.tbSpreadSheetID.ReadOnly = true;
            this.tbSpreadSheetID.Size = new System.Drawing.Size(575, 20);
            this.tbSpreadSheetID.TabIndex = 1;
            this.tbSpreadSheetID.Text = "Sheet URL";
            this.tbSpreadSheetID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(588, 91);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(105, 33);
            this.btSave.TabIndex = 2;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(224, 12);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(135, 20);
            this.textBox3.TabIndex = 1;
            this.textBox3.Text = "Chia sẻ sheet với email";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbSheetEmailShare
            // 
            this.tbSheetEmailShare.Location = new System.Drawing.Point(365, 12);
            this.tbSheetEmailShare.Name = "tbSheetEmailShare";
            this.tbSheetEmailShare.ReadOnly = true;
            this.tbSheetEmailShare.Size = new System.Drawing.Size(328, 20);
            this.tbSheetEmailShare.TabIndex = 1;
            this.tbSheetEmailShare.Text = "@";
            this.tbSheetEmailShare.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fGoogleSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 142);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.tbSpreadSheetID);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.tbSheetURL);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.tbSheetEmailShare);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cbSheetTrangThai);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fGoogleSheet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Post dữ liệu lên google sheet";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.fGoogleSheet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSheetTrangThai;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tbSheetURL;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox tbSpreadSheetID;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox tbSheetEmailShare;
    }
}