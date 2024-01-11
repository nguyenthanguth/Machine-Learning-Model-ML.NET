namespace ThaiDanh
{
    partial class fPrintComputer
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
            this.cbList = new System.Windows.Forms.CheckedListBox();
            this.btOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbList
            // 
            this.cbList.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbList.FormattingEnabled = true;
            this.cbList.Items.AddRange(new object[] {
            "Print 1",
            "Print 2"});
            this.cbList.Location = new System.Drawing.Point(0, 0);
            this.cbList.Name = "cbList";
            this.cbList.Size = new System.Drawing.Size(484, 214);
            this.cbList.TabIndex = 0;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(375, 231);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(109, 34);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "Tiếp tục";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // fPrintComputer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 277);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.cbList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fPrintComputer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn máy in";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.CheckedListBox cbList;
    }
}